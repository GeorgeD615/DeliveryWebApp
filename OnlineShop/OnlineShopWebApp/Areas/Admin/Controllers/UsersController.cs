using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models.Helpers;
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
            return View(users.Select(ModelConverter.ConvertToUserViewModel).ToList());
        }

        public IActionResult ShowUser(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            return View(ModelConverter.ConvertToUserViewModel(user));
        }

        public IActionResult ChangePassword(Guid userId) => View(new ChangePasswordViewModel() { UserId = userId });

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            usersRepository.ChangePassword(model.UserId, model.Password);

            return RedirectToAction("ShowUser", new { model.UserId });
        }

        public IActionResult Edit(Guid userId)
        {
            var user = usersRepository.TryGetById(userId);
            return View(new UserViewModel() { Id = userId, Login = user.Login, Role = ModelConverter.ConvertToRoleViewModel(user.Role)});
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel userVM)
        {
            var existingUser = usersRepository.TryGetByLogin(userVM.Login);

            if (existingUser != null)
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            if (!ModelState.IsValid)
                return View(userVM);

            var userModel = new User()
            {
                Id = userVM.UserId,
                Login = userVM.Login,
                Role = rolesRepository.TryGetById(userVM.RoleId)
            };

            usersRepository.Edit(userModel);

            return RedirectToAction("ShowUser", new { userVM.UserId });
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

            var user = new User()
            {
                Login = createUserViewModel.RegistrationModel.Login,
                Password = createUserViewModel.RegistrationModel.Password,
                Role = rolesRepository.TryGetById(createUserViewModel.RoleId)
            };

            usersRepository.Add(user);

            return RedirectToAction("Index");
        }
    }
}
