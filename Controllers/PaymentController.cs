using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewStripeApp.Data;
using NewStripeApp.Models;
using Stripe.Checkout;

namespace NewStripeApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeContext _context;

        public PaymentController(StripeContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        [HttpPost]
        public ActionResult ConfirmCheckout([Bind("Id, Name, ImageUrl, PriceId")] Item Item)
        {
            var domain = "https://localhost:7178";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = $"{Item.PriceId}",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Payment/Success",
                CancelUrl = domain + "/Payment/Cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        //[HttpGet]
        //public async Task<ActionResult> Checkout(int? id)
        //{
        //    if (id == null || _context.Items == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Items.FindAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(item);
        //}

        public ActionResult Cancel()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
