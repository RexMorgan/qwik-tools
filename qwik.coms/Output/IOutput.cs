namespace qwik.coms.Output
{
    public interface IOutput
    {
        void Formatted(string message, params object[] args);
    }
}