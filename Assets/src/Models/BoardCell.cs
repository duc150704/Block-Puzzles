public class BoardCell
{
    public GridPosition GridPosition {  get; private set; }
    public bool IsEmpty { get; private set; } = true;
    
    public BoardCell(GridPosition gridPosition)
        => this.GridPosition = gridPosition;

    public void Occupy()
        => IsEmpty = false;
    
    public void Free()
        => IsEmpty = true;
}
