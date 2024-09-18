using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IIdentityHandler
{
    Task<LoginUserResponse> LoginAsync(LoginUserRequest request);
    Task Logout();
    Task<LoginUserResponse> CreateAsync(CreateUserRequest request);
    Task UpdateEmailAsync(UpdateUserEmailRequest request);
    Task UpdatePasswordAsync(UpdateUserPasswordRequest request);
    Task DeleteAsync(DeleteUserRequest request);
    Task<UserResponse> GetByIdAsync(GetUserByIdRequest request);
    Task<PagedResponse<UserResponse>> GetAllAsync(GetAllUsersRequest request);
    Task AddOrUpdateUserClaimAsync(AddOrUpdateUserClaimRequest request);
}