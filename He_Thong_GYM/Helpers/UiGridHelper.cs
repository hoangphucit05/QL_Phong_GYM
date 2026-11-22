using System.ComponentModel;

namespace He_Thong_GYM.Helpers;

/// <summary>
/// BindingList có hỗ trợ sort để DataGridView hiển thị mũi tên và sắp xếp
/// ngay khi người dùng nhấn vào tiêu đề cột.
/// </summary>
public sealed class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private PropertyDescriptor? _sortProperty;
    private ListSortDirection _sortDirection;

    public SortableBindingList(IEnumerable<T> items) : base(items.ToList())
    {
    }

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;
    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var items = Items.ToList();
        items.Sort((left, right) =>
        {
            var result = SoSanh(prop.GetValue(left), prop.GetValue(right));
            return direction == ListSortDirection.Ascending ? result : -result;
        });

        RaiseListChangedEvents = false;
        for (var i = 0; i < items.Count; i++)
        {
            Items[i] = items[i];
        }
        RaiseListChangedEvents = true;

        _sortProperty = prop;
        _sortDirection = direction;
        _isSorted = true;
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }

    private static int SoSanh(object? left, object? right)
    {
        if (ReferenceEquals(left, right)) return 0;
        if (left is null) return -1;
        if (right is null) return 1;

        if (left is IComparable comparable)
        {
            try
            {
                return comparable.CompareTo(right);
            }
            catch (ArgumentException)
            {
                // Một số kiểu khác nhau không thể CompareTo; dùng chuỗi bên dưới.
            }
        }

        return string.Compare(left.ToString(), right.ToString(), StringComparison.CurrentCultureIgnoreCase);
    }
}

public static class UiGridHelper
{
    public static void DatNguon<T>(DataGridView grid, IEnumerable<T>? duLieu)
    {
        grid.DataSource = new SortableBindingList<T>(duLieu ?? Enumerable.Empty<T>());
        BatSapXep(grid);
    }

    public static void BatSapXep(DataGridView grid)
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (!string.IsNullOrWhiteSpace(column.DataPropertyName))
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
    }

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
