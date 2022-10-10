using FluentResults;

namespace IMDb.Application.Extension;
public class ApplicationError : Error
{
	public ApplicationError(string error) : base(error)
	{
	}
}