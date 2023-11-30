using Microsoft.AspNetCore.Mvc;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public interface IAuthService
	{
		Task<ServiceResponse> Login(LoginDto loginDto);

	}
}
