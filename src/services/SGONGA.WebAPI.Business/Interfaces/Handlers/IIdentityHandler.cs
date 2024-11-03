using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IIdentityHandler
{
    Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request);
    Task<Result> Logout();
    Task<Result<LoginUserResponse>> CreateAsync(CreateUserRequest request);
    Task<Result> UpdateEmailAsync(UpdateUserEmailRequest request);
    Task<Result> UpdatePasswordAsync(UpdateUserPasswordRequest request);
    Task<Result> DeleteAsync(DeleteUserRequest request);
    Task<Result<UserResponse>> GetByIdAsync(GetUserByIdRequest request);
    Task<Result<BasePagedResponse<UserResponse>>> GetAllAsync(GetAllUsersRequest request);
    Task<Result> AddOrUpdateUserClaimAsync(AddOrUpdateUserClaimRequest request);
}