using DevExpressDomain;
using DevExpressDomain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DevExpressExercise.Controllers
{
    public class CustomerController : Controller
    {
        private readonly List<Customer> _customers = new List<Customer>
    {
        new Customer { Id = 1, Name = "Mohammed", Phone = "0512345678", Status = CustomerStatus.Active },
        new Customer { Id = 2, Name = "Othman", Phone = "0512345679", Status = CustomerStatus.Active }
    };

        public IActionResult Index()
        {
            return View(_customers);
        }

        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Add(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                newCustomer.Id = _customers.Max(c => c.Id) + 1;
                _customers.Add(newCustomer);
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(int id, Customer updatedCustomer)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            if (!customer.IsNew && updatedCustomer.Phone != customer.Phone)
            {
                ModelState.AddModelError("Phone", "Phone can't be changed for existing customers.");
                return View(updatedCustomer);
            }

            if (customer.Status == CustomerStatus.Banned)
            {
                return BadRequest("Cannot edit banned customers.");
            }

            customer.Name = updatedCustomer.Name;
            customer.Phone = updatedCustomer.Phone;
            return RedirectToAction("Index");
        }

        public IActionResult SetStatus(int id, CustomerStatus newStatus)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            if (customer.Status == CustomerStatus.Banned)
            {
                return BadRequest("Cannot change status for banned customers.");
            }

            customer.Status = newStatus;
            return RedirectToAction("Index");
        }

        // ... Other controller actions as needed
    }



}
