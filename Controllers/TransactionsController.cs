using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCRMShield.Data;
using SmartCRMShield.Models;

namespace SmartCRMShield.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin"))
            {
                var transactions = await _context.Transactions.ToListAsync();
                ViewBag.TotalTransactions = transactions.Count;
                ViewBag.TotalFraudAlerts = transactions.Count(t => t.IsFraudAlert);
                return View(transactions);
            }

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                return Problem("User information is not available.");
            }

            var clientTransactions = await _context.Transactions
                .Where(t => t.UserEmail == user.Email)
                .ToListAsync();

            ViewBag.TotalTransactions = clientTransactions.Count;
            ViewBag.TotalAmountSpent = clientTransactions.Sum(t => t.Amount);
            return View(clientTransactions);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index"); // Admin cannot create transactions
            }

            var userEmail = User.Identity?.Name;
            ViewBag.UserEmail = userEmail;

            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserEmail,Amount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Date = DateTime.Now;
                transaction.IsFraudAlert = transaction.Amount > 1000;
                transaction.IsBlocked = false;

                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // POST: Transactions/ToggleBlock/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleBlock(int id)
        {
            var txn = await _context.Transactions.FindAsync(id);
            if (txn == null)
            {
                return NotFound();
            }

            txn.IsBlocked = !txn.IsBlocked;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
