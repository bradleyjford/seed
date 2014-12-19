using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Seed.Admin.Users;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Infrastructure.Data;
using Seed.Security;

namespace Seed.Web.Handlers.Admin.Users
{
    [RouteArea(Areas.Admin)]
    [RoutePrefix("manage-users")]
    [Route("{ action = Index }")]
    public class UsersController : Controller
    {
        static UsersController()
        {
            // Input model to command mappings
            Mapper.CreateMap<EditUserInputModel, EditUserCommand>();

            // Query to response mappings

        }

        private readonly ICommandBus _mediator;
        private readonly ISeedDbContext _dbContext;

        public UsersController(
            ICommandBus mediator,
            ISeedDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("items")]
        [HttpPost]
        public async Task<ActionResult> GetItems(
            string filterText,
            PagingOptions pagingOptions)
        {
            var usersListQuery = new UsersListQuery(filterText, pagingOptions);

            var users = await _mediator.Execute(usersListQuery);

            return Json(users);
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            var viewModel = Mapper.Map<EditUserViewModel>(user);

            return View(viewModel);
        }

        [Route("{id:Guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, EditUserInputModel inputModel)
        {
            var command = Mapper.Map<EditUserCommand>(inputModel);

            command.UserId = id;

            var result = await _mediator.Execute(command);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [Route("{id:Guid}/activate")]
        [HttpPost]
        public async Task<ActionResult> Activate(Guid id)
        {
            var command = new ActivateUserCommand(id);

            var result = await _mediator.Execute(command);

            return View(result);
        }

        [Route("{id:Guid}/deactivate")]
        [HttpPost]
        public async Task<ActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateUserCommand(id);

            var result = await _mediator.Execute(command);

            return View(result);
        }
    }
}
