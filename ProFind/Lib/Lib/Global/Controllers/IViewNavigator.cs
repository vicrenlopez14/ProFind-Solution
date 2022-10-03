namespace ProFind.Lib.Global.Controllers
{
    public interface IViewNavigator<in TV, in TCc>
    {
        void Init(TCc centralController);
        bool GoBack();
        void NavigateTo(TV view);
    }
}