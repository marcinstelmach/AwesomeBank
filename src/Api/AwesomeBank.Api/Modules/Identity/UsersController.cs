namespace AwesomeBank.Api.Modules.Identity
{
    using System.Net;
    using System.Threading.Tasks;
    using AwesomeBank.Api.Filters;
    using AwesomeBank.Api.Models;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.Api.Permissions;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UsersController(IMapper mapper, IBus bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        [HttpPost]
        [ValidateModelFilter]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [Authorize(AccountPermissions.Manage)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel viewModel)
        {
            var command = _mapper.Map<CreateUserViewModel, CreateUserCommand>(viewModel);
            await _bus.ExecuteCommandAsync(command);
            return Accepted();
        }
    }
}