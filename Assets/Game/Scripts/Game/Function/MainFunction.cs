namespace CarGo.Game.Function
{
    public class MainFunction: Function
    {
        protected override int MaxCommand  => 16;
        public override FunctionType FunctionType => FunctionType.Main;
    }
}