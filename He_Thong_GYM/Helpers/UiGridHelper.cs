namespace He_Thong_GYM.Helpers;

public static class UiGridHelper
{
    /// <summary>
    /// Bỏ cả vùng chọn lẫn ô hiện hành. ClearSelection() đơn thuần vẫn để lại
    /// CurrentRow, khiến Form tưởng người dùng đã chọn dòng đầu tiên.
    /// </summary>
    public static void BoChon(DataGridView grid)
    {
        grid.ClearSelection();
        grid.CurrentCell = null;
    }
}
