namespace OpenPay.Interfaces.Tests;

public abstract class ArrangeAct
{
    protected ArrangeAct()
    {
        Arrange();

        Act();
    }

    protected virtual void Arrange() { }

    protected virtual void Act() { }
}