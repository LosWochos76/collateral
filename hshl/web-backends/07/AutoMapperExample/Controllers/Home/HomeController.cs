using Microsoft.AspNetCore.Mvc;
using AutoMapper;

public class HomeController(IMapper mapper) : Controller
{
    public ActionResult Index()
    {
        var order = new OrderDTO(Guid.NewGuid(), 13.64, 12);
        var orderViewModel = mapper.Map<OrderViewModel>(order);

        return View(orderViewModel);
    }
}