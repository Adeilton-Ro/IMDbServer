using FluentResults;

namespace IMDb.Application.Extension;
public class ValidationError : Error
{
	public Dictionary<string, string[]> ErrorMessageDictionary { get; }
	public ValidationError(Dictionary<string, string[]> errorMessageDictionary) : base("A validation error happened")
	{
		ErrorMessageDictionary = errorMessageDictionary;
	}

}