namespace CarGo.Game.Function
{
    public class SubFunction : Function
    {
        protected override int MaxCommand => 10;
        public override FunctionType FunctionType => FunctionType.Procedure;
    }
}