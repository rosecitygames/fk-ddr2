namespace IndieDevTools.States
{
    public class SimpleState : AbstractState
    {
        public static IState Create(string name)
        {
            IState state = new SimpleState
            {
                stateName = name
            };

            return state;
        }
    }
}