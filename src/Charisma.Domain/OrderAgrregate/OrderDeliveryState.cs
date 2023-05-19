namespace Charisma.Domain;

public enum OrderDeliveryState
{
    Ready = 1,
    InProgress = 2,
    Sent = 3,
    Delivered = 4,
    Cancel = 5
}