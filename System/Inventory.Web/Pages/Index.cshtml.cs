using Inventory.Core.Repositories;
using Inventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventory.Web.Pages;

public class IndexModel : PageModel
{
	private readonly ProductRepository _products;

	public IList<Product> Products { get; private set; } = new List<Product>();
	[BindProperty(SupportsGet = true)]
	public string? Query { get; set; }

	public IndexModel(ProductRepository products)
	{
		_products = products;
	}

	public void OnGet()
	{
		Products = string.IsNullOrWhiteSpace(Query) ? _products.GetAll() : _products.Search(Query!);
	}
}