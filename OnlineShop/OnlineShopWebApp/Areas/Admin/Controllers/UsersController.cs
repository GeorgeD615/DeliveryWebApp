using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models.Roles;
using OnlineShopWebApp.Models.Users;
using OnlineShopWebApp.Models.ViewModels;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository usersRepository;
        private readonly IRolesRepository rolesRepository;
        public UsersController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
        }

        public IActionResult Index()
        {
            var users = usersRepository.GetAll();
            return View(users);
        }

        public IActionResult ShowUser(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            return View(user);
        }

        public IActionResult ChangePassword(Guid userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(changePasswordViewModel);

            usersRepository.ChangePassword(changePasswordViewModel.UserId, changePasswordViewModel.Password);

            return RedirectToAction("ShowUser", new { changePasswordViewModel.UserId });
        }

        public IActionResult Edit(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            return View(new EditUserViewModel() { UserId = userId, Login = user.Login, RoleId = user.Role.Id });
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel editUserViewModel)
        {
            var existingUser = usersRepository.TryGetByLogin(editUserViewModel.Login);

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(editUserViewModel);

            var editModel = new EditUserModel()
            {
                Login = editUserViewModel.Login,
                UserId = editUserViewModel.UserId,
                Role = rolesRepository.TryGetById(editUserViewModel.RoleId)
            };

            usersRepository.Edit(editModel);

            return RedirectToAction("ShowUser", new { editUserViewModel.UserId });
        }

        public IActionResult Remove(Guid userId)
        {
            usersRepository.Remove(userId);
            return RedirectToAction("Index");
        }

        public IActionResult Create() => View(new CreateUserViewModel() { RegistrationModel = new RegistrationViewModel()});

        [HttpPost]
        public IActionResult Create(CreateUserViewModel createUserViewModel)
        {
            if (createUserViewModel.RegistrationModel.Password == createUserViewModel.RegistrationModel.Login)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");

            var existingUser = usersRepository.TryGetByLogin(createUserViewModel.RegistrationModel.Login);

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(createUserViewModel);

            var user = new User(
                createUserViewModel.RegistrationModel.Login, 
                createUserViewModel.RegistrationModel.Password,
                rolesRepository.TryGetById(createUserViewModel.RoleId));

            usersRepository.Add(user);

            return RedirectToAction("Index");
        }
    }
}
