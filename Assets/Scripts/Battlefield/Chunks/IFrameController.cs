namespace Battlefield.Chunks
{
    public interface IFrameController
    {
        void UpdateFrameColors(int direction);
        void SetDefaultFrameColor();
        void SetHoverColor();
    }
}
