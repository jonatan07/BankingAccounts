using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAccounts.Api.Controllers;

/// <summary>
/// Expone operaciones CRUD para administrar cuentas bancarias.
/// </summary>
[ApiController]
[Authorize]
[Route("api/accounts")]
[Produces("application/json")]
public sealed class AccountsController(IBankAccountService bankAccountService) : ControllerBase
{
    /// <summary>
    /// Obtiene todas las cuentas bancarias registradas.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la solicitud en curso.</param>
    /// <returns>Lista de cuentas bancarias.</returns>
    /// <response code="200">Retorna la lista de cuentas bancarias.</response>
    /// <response code="401">La solicitud no incluye un token JWT valido.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BankAccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<BankAccountDto>>> GetAll(CancellationToken cancellationToken)
    {
        var accounts = await bankAccountService.GetAllAsync(cancellationToken);
        return Ok(accounts);
    }

    /// <summary>
    /// Obtiene una cuenta bancaria por su identificador.
    /// </summary>
    /// <param name="id">Identificador unico de la cuenta bancaria.</param>
    /// <param name="cancellationToken">Token para cancelar la solicitud en curso.</param>
    /// <returns>Cuenta bancaria solicitada.</returns>
    /// <response code="200">Retorna la cuenta bancaria encontrada.</response>
    /// <response code="401">La solicitud no incluye un token JWT valido.</response>
    /// <response code="404">No existe una cuenta bancaria con el identificador indicado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BankAccountDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await bankAccountService.GetByIdAsync(id, cancellationToken);
        return result.Success ? Ok(result.Value) : NotFound(new { result.Error });
    }

    /// <summary>
    /// Crea una nueva cuenta bancaria.
    /// </summary>
    /// <param name="request">Datos necesarios para crear la cuenta bancaria.</param>
    /// <param name="cancellationToken">Token para cancelar la solicitud en curso.</param>
    /// <returns>Cuenta bancaria creada.</returns>
    /// <response code="201">La cuenta bancaria fue creada correctamente.</response>
    /// <response code="400">La solicitud contiene datos invalidos.</response>
    /// <response code="401">La solicitud no incluye un token JWT valido.</response>
    /// <response code="409">Ya existe una cuenta bancaria con el mismo numero de cuenta.</response>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BankAccountDto>> Create(CreateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await bankAccountService.CreateAsync(request, cancellationToken);
        if (!result.Success)
        {
            return Conflict(new { result.Error });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    /// <summary>
    /// Actualiza una cuenta bancaria existente.
    /// </summary>
    /// <param name="id">Identificador unico de la cuenta bancaria.</param>
    /// <param name="request">Datos actualizados de la cuenta bancaria.</param>
    /// <param name="cancellationToken">Token para cancelar la solicitud en curso.</param>
    /// <returns>Cuenta bancaria actualizada.</returns>
    /// <response code="200">La cuenta bancaria fue actualizada correctamente.</response>
    /// <response code="400">La solicitud contiene datos invalidos.</response>
    /// <response code="401">La solicitud no incluye un token JWT valido.</response>
    /// <response code="404">No existe una cuenta bancaria con el identificador indicado.</response>
    /// <response code="409">Ya existe una cuenta bancaria con el mismo numero de cuenta.</response>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BankAccountDto>> Update(Guid id, UpdateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await bankAccountService.UpdateAsync(id, request, cancellationToken);
        if (!result.Success)
        {
            return result.Error == "Account not found."
                ? NotFound(new { result.Error })
                : Conflict(new { result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Elimina una cuenta bancaria por su identificador.
    /// </summary>
    /// <param name="id">Identificador unico de la cuenta bancaria.</param>
    /// <param name="cancellationToken">Token para cancelar la solicitud en curso.</param>
    /// <returns>Resultado de la eliminacion.</returns>
    /// <response code="204">La cuenta bancaria fue eliminada correctamente.</response>
    /// <response code="401">La solicitud no incluye un token JWT valido.</response>
    /// <response code="404">No existe una cuenta bancaria con el identificador indicado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await bankAccountService.DeleteAsync(id, cancellationToken);
        return result.Success ? NoContent() : NotFound(new { result.Error });
    }
}
