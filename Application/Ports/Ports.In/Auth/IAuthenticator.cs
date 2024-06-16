using Domain.Core.Session;

namespace Ports.In.Auth;

public interface IAuthenticator
{
    /// <summary>
    /// Kiểm tra thông tin đăng nhập của người dùng. Ngoài việc kiểm tra thông tin có đúng hay không,
    /// hàm còn trả lại thông tin thời gian người dùng đổi mật khẩu nếu họ nhập mật khẩu cũ
    /// </summary>
    /// <param name="phoneOrEmail"></param>
    /// <param name="password"></param>
    /// <returns><see cref="SessionId" /> và <see cref="SessionUser" /></returns>
    public Task<SessionUser> Login(string phoneOrEmail, string password);
}