using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Interactivity;

namespace VirtualWallet.Behaviors
{
    /// <summary>
    /// Behavior which enables showing/hiding of a pivot item
    /// </summary>
    public class HideablePivotItemBehavior : Behavior<PivotItem>
    {
        public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register(
            "Visible",
            typeof(bool),
            typeof(HideablePivotItemBehavior),
            new PropertyMetadata(true, VisiblePropertyChanged));

        private Pivot parentPivot;

        private PivotItem pivotItem;

        private int previousPivotItemIndex;

        private int lastPivotItemsCount;

        public bool Visible
        {
            get => (bool)GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }

        protected override void OnAttached()
        {
            pivotItem = AssociatedObject;
            base.OnAttached();
        }

        private static void VisiblePropertyChanged(DependencyObject dpObj, DependencyPropertyChangedEventArgs change)
        {
            if (change.NewValue.GetType() != typeof(bool) || dpObj.GetType() != typeof(HideablePivotItemBehavior))
                return;

            var behavior = (HideablePivotItemBehavior)dpObj;
            var pivotItem = behavior.pivotItem;

            // Parent pivot has to be assigned after the visual tree is initialized
            if (behavior.parentPivot == null)
            {
                behavior.parentPivot = (Pivot)behavior.pivotItem.Parent;
                // if the parent is null return
                if (behavior.parentPivot == null)
                    return;
            }

            var parentPivot = behavior.parentPivot;
            if (!(bool)change.NewValue)
            {
                if (parentPivot.Items.Contains(behavior.pivotItem))
                {
                    behavior.previousPivotItemIndex = parentPivot.Items.IndexOf(pivotItem);
                    parentPivot.Items.Remove(pivotItem);
                    behavior.lastPivotItemsCount = parentPivot.Items.Count;
                }
            }
            else
            {
                if (!parentPivot.Items.Contains(pivotItem))
                {
                    if (behavior.lastPivotItemsCount >= parentPivot.Items.Count)
                        parentPivot.Items.Insert(behavior.previousPivotItemIndex, pivotItem);
                    else
                        parentPivot.Items.Add(pivotItem);
                }
            }
        }
    }
}
