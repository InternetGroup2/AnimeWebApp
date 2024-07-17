using Microsoft.AspNetCore.Mvc;

public class ContactUsController : Controller
{
    private readonly EmailService _emailService;

    public ContactUsController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ContactUsViewModel model)
    {
        if (ModelState.IsValid)
        {
            string subject = "Contact Us Form Submission";
            string body = $"Name: {model.Name}\nEmail: {model.Email}\nComment: {model.Comment}";

            _emailService.SendEmail("animateverse8@gmail.com", subject, body);

             ViewBag.Message = "Your message has been sent successfully. Redirecting in 5 seconds...";
            ViewBag.Redirect = true; 
            
            return View();
        }

        return View(model);
    }
}
