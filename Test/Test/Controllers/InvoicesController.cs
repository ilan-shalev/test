using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test.Data.Entities;
using Test.Data.Repositories;
using Test.Dtos;

namespace Test.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly InvoicesRepository _invoicesRepository;

    public InvoicesController(IMapper mapper, InvoicesRepository invoicesRepository)
    {
        _mapper = mapper;
        _invoicesRepository = invoicesRepository;
    }

    [HttpPost("/all")]
    public async Task<ActionResult<IEnumerable<InvoiceReadDto>>> GetAll(InvoicesFilterParameters parameters)
    {
        var invoices = await _invoicesRepository.GetAll(invoice =>
            parameters.FromTime.HasValue ? invoice.TimeOfPurchase > parameters.FromTime : true &&
            parameters.ToTime.HasValue ? invoice.TimeOfPurchase < parameters.ToTime : true &&
            !string.IsNullOrEmpty(parameters.Recipient) ? invoice.Recipient.ToLower().Contains(parameters.Recipient.ToLower().Trim()) : true
        , p => p.Products) ; 

        return Ok(_mapper.Map<IEnumerable<InvoiceReadDto>>(invoices));
    }

    [HttpGet("/{id}", Name = nameof(GetById))]
    public async Task<ActionResult<InvoiceReadDto>> GetById(int id)
    {
        var invoice = await _invoicesRepository.Get(id, true);

        return invoice is null ? NotFound() : _mapper.Map<InvoiceReadDto>(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice(InvoiceCreateDto dto)
    {
        var invoice = new Invoice
        {
            TimeOfPurchase = DateTime.UtcNow,
            Recipient = dto.Recipient,
            Products = _mapper.Map<IEnumerable<Product>>(dto.Products).ToList()
        };

        await _invoicesRepository.Add(invoice);

        return CreatedAtAction(nameof(GetById), new { invoice.Id}, invoice);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateInvoice(InvoiceUpdateDto dto)
    {
        var invoiceFromDb = await _invoicesRepository.Get(dto.Id, true);
        if (invoiceFromDb is null)
            return NotFound();

        await _invoicesRepository.Update(_mapper.Map<Invoice>(dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(Guid id)
    {
        var invoiceFromDb = await _invoicesRepository.Get(id, true);
        if (invoiceFromDb is null)
            return NotFound();

        await _invoicesRepository.Remove(invoiceFromDb);
        return NoContent();
    }    
}
