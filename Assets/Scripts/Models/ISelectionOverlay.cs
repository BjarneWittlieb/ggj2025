namespace Models
{
    public interface ISelectionOverlay
    {
        void Setup(BubbleType bubbleType);

        void Render();

        void Destroy();
    }
}