
/// <summary>
/// Summary description for IMessage
/// </summary>
namespace com.Sconit.Web
{
    public interface IMessage
    {
        void ShowSuccessMessage(string message);

        void ShowSuccessMessage(string message, params string[] paramters);

        void ShowWarningMessage(string message);

        void ShowWarningMessage(string message, params string[] paramters);

        void ShowErrorMessage(string message);

        void ShowErrorMessage(string message, params string[] paramters);

        void CleanMessage();
    }
}
