
Imports System.Collections.ObjectModel
Imports System.Windows.Controls.Primitives

<TemplatePart(Name:="PART_ListBox", Type:=GetType(ListBox))>
<TemplatePart(Name:="PART_PopupBorder", Type:=GetType(Border))>
<TemplatePart(Name:="PART_Popup", Type:=GetType(Popup))>
Public Class AutoInsertPopupControl
    Inherits Control


    Public Shared ReadOnly ItemSelectedEvent As RoutedEvent

    Private _popupBorder As Border
    Private _listBox As ListBox
    Private _popUp As Popup

    Private isRecording As Boolean
    Private recordStartPosition As Integer
    Private currentFilterString As String



    Shared Sub New()
        'Mit dem OverrideMetadata-Aufruf wird dem System mitgeteilt, dass das Element einen Stil bereitstellen möchte, der sich von seiner Basisklasse unterscheidet.
        'Dieser Stil ist unter "Themes\Generic.xaml" definiert.
        DefaultStyleKeyProperty.OverrideMetadata(GetType(AutoInsertPopupControl), New FrameworkPropertyMetadata(GetType(AutoInsertPopupControl)))

        ItemSelectedEvent = EventManager.RegisterRoutedEvent("ItemSelected", RoutingStrategy.Bubble, GetType(RoutedEventArgs), GetType(AutoInsertPopupControl))

    End Sub


    Public Custom Event ItemSelected As RoutedEventHandler
        AddHandler(value As RoutedEventHandler)
            Me.AddHandler(ItemSelectedEvent, value)
        End AddHandler
        RemoveHandler(value As RoutedEventHandler)
            Me.RemoveHandler(ItemSelectedEvent, value)
        End RemoveHandler
        RaiseEvent(sender As Object, e As RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event


#Region "Properties"

    Public Property TargetControl As FrameworkElement
        Get
            Return CType(GetValue(TargetControlProperty), FrameworkElement)
        End Get

        Set(ByVal value As FrameworkElement)
            SetValue(TargetControlProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TargetControlProperty As DependencyProperty =
                           DependencyProperty.Register("TargetControl",
                           GetType(FrameworkElement), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing, AddressOf TargetControlProperty_Changed))

    Private Shared Sub TargetControlProperty_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If e.NewValue IsNot Nothing Then
            DirectCast(e.NewValue, TextBoxBase).Tag = d
            AddHandler DirectCast(e.NewValue, TextBoxBase).PreviewKeyDown, AddressOf TargetControlProperty_KeyDown
            AddHandler DirectCast(e.NewValue, TextBoxBase).PreviewKeyUp, AddressOf TargetControlProperty_KeyUp
        End If
    End Sub

    Private Shared Sub TargetControlProperty_KeyDown(sender As Object, e As KeyEventArgs)
        Dim lb = TryCast(DirectCast(sender, TextBoxBase).Tag, AutoInsertPopupControl)
        If lb Is Nothing Then Exit Sub

        If e.Key = CType(lb.GetValue(FocusKeyProperty), Key) Then
            lb._listBox.UpdateLayout()
            lb._listBox.Focus()
            Keyboard.Focus(lb._listBox)
        End If
    End Sub
    Private Shared Sub TargetControlProperty_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
        Dim lb = TryCast(DirectCast(sender, TextBoxBase).Tag, AutoInsertPopupControl)
        If lb Is Nothing OrElse DirectCast(lb.GetValue(OpenPopupTriggerCharProperty), Char) = Char.MinValue Then Exit Sub

        Dim lastChar As Char
        If DirectCast(sender, TextBox).CaretIndex = 0 Then Exit Sub
        lastChar = CChar(DirectCast(sender, TextBox).Text.Substring(DirectCast(sender, TextBox).CaretIndex - 1, 1))


        If lastChar = DirectCast(lb.GetValue(OpenPopupTriggerCharProperty), Char) Then
            lb.SetValue(IsPopUpOpenProperty, True)
            lb.isRecording = True
            lb.recordStartPosition = DirectCast(sender, TextBox).CaretIndex
            Debug.WriteLine("Set recoding to True")
        End If
        If lb.isRecording Then
            If DirectCast(sender, TextBox).CaretIndex < lb.recordStartPosition Then
                lb.isRecording = False : lb.recordStartPosition = 0
                lb.SetValue(IsPopUpOpenProperty, False)
            Else
                lb.currentFilterString = DirectCast(sender, TextBox).Text.Substring(lb.recordStartPosition, DirectCast(sender, TextBox).CaretIndex - lb.recordStartPosition)
                Debug.WriteLine("Recorded Filter: " & lb.currentFilterString)
                'Filtern
                Dim fullList = DirectCast(lb.GetValue(AutoInsertListProperty), IEnumerable)
                If TryCast(fullList, IEnumerable(Of IAutoInsertItem)) IsNot Nothing Then
                    lb.SetValue(VisibleItemsProperty, DirectCast(lb.GetValue(AutoInsertListProperty),
                            IEnumerable(Of IAutoInsertItem)).Where(Function(x) x.SearchStringContent.ToLower.Contains(lb.currentFilterString.ToLower)))
                Else
                    If TryCast(fullList, List(Of String)) Is Nothing Then
                        Throw New Exception("The collection must be an IAutoInsertItem-Collection or a collection of string")
                    End If

                    Dim list = New List(Of IAutoInsertItem)
                    DirectCast(fullList, IEnumerable(Of String)).ToList.ForEach(Sub(x) list.Add(New AutoInsertItem(x)))
                    lb.SetValue(VisibleItemsProperty, list.Where(Function(x) x.SearchStringContent.ToLower.Contains(lb.currentFilterString.ToLower)))
                End If

            End If

        End If

        'Debug.WriteLine(e.Key.ToString())
    End Sub



    Public Overloads Property Background As Brush
        Get
            Return CType(GetValue(BackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(BackgroundProperty, value)
        End Set
    End Property

    Public Shared Shadows ReadOnly BackgroundProperty As DependencyProperty =
                           DependencyProperty.Register("Background",
                           GetType(Brush), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(New SolidColorBrush(Colors.White)))



    Public Property OpenPopupTriggerChar As Char
        Get
            Return CChar(GetValue(OpenPopupTriggerCharProperty))
        End Get

        Set(ByVal value As Char)
            SetValue(OpenPopupTriggerCharProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OpenPopupTriggerCharProperty As DependencyProperty =
                           DependencyProperty.Register("OpenPopupTriggerChar",
                           GetType(Char), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))


    Public Property ReplaceTriggerChar As Boolean
        Get
            Return CBool(GetValue(ReplaceTriggerCharProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(ReplaceTriggerCharProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ReplaceTriggerCharProperty As DependencyProperty =
                           DependencyProperty.Register("ReplaceTriggerChar",
                           GetType(Boolean), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(True))



    Public Property FocusKey As Key
        Get
            Return CType(GetValue(FocusKeyProperty), Key)
        End Get

        Set(ByVal value As Key)
            SetValue(FocusKeyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FocusKeyProperty As DependencyProperty =
                           DependencyProperty.Register("FocusKey",
                           GetType(Key), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Key.Down))



    Public Property IsPopUpOpen As Boolean
        Get
            Return CBool(GetValue(IsPopUpOpenProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(IsPopUpOpenProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsPopUpOpenProperty As DependencyProperty =
                           DependencyProperty.Register("IsPopUpOpen",
                           GetType(Boolean), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(False, AddressOf IsPopUpOpenProperty_Changed))

    Private Shared Sub IsPopUpOpenProperty_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)

    End Sub

    Public Sub FocusListbox()
        Dim lb = _listBox
        lb.UpdateLayout()
        lb.Focus()
        Keyboard.Focus(lb)
    End Sub

    Public Property AutoInsertList As IEnumerable
        Get
            Return CType(GetValue(AutoInsertListProperty), IEnumerable)
        End Get

        Set(ByVal value As IEnumerable)
            SetValue(AutoInsertListProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AutoInsertListProperty As DependencyProperty =
                           DependencyProperty.Register("AutoInsertList",
                           GetType(IEnumerable), GetType(AutoInsertPopupControl), New PropertyMetadata(Nothing, AddressOf AutoInsertListProperty_Changed))
    Private Shared Sub AutoInsertListProperty_Changed(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim autoInserCtl = DirectCast(d, AutoInsertPopupControl)
        If TryCast(e.NewValue, IEnumerable(Of IAutoInsertItem)) IsNot Nothing Then
            autoInserCtl.SetValue(VisibleItemsProperty, e.NewValue)
        Else
            If TryCast(e.NewValue, List(Of String)) Is Nothing Then
                Throw New Exception("The collection must be an IAutoInsertItem-Collection or a collection of string")
            End If

            Dim list = New List(Of IAutoInsertItem)
            DirectCast(e.NewValue, IEnumerable(Of String)).ToList.ForEach(Sub(x) list.Add(New AutoInsertItem(x)))
            autoInserCtl.SetValue(VisibleItemsProperty, New List(Of IAutoInsertItem))
        End If


    End Sub



    Public Property SelectedInsertListItem As IAutoInsertItem
        Get
            Return CType(GetValue(SelectedInsertListItemProperty), IAutoInsertItem)
        End Get

        Set(ByVal value As IAutoInsertItem)
            SetValue(SelectedInsertListItemProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedInsertListItemProperty As DependencyProperty =
                           DependencyProperty.Register("SelectedInsertListItem",
                           GetType(IAutoInsertItem), GetType(AutoInsertPopupControl),
                           New FrameworkPropertyMetadata(Nothing, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AddressOf SelectedInsertListItemProperty_Changed))

    Private Shared Sub SelectedInsertListItemProperty_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim autoInsertControl = DirectCast(d, AutoInsertPopupControl)
        Dim ctl As TextBox = CType(autoInsertControl.GetValue(TargetControlProperty), TextBox)
        If e.NewValue IsNot Nothing Then
            Dim triggerChar = CChar(autoInsertControl.GetValue(OpenPopupTriggerCharProperty))
            Dim replaceTriggerChar As Boolean = CBool(autoInsertControl.GetValue(ReplaceTriggerCharProperty))
            Dim replaceString = If(replaceTriggerChar, triggerChar.ToString, "") & autoInsertControl.currentFilterString
            If replaceString.Length = 0 Then
                ctl.Text = ctl.Text.Insert(autoInsertControl.recordStartPosition, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
            Else
                ctl.Text = ctl.Text.Replace(replaceString, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
            End If

            ctl.CaretIndex = autoInsertControl.recordStartPosition + DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString.Length

            autoInsertControl.SetValue(SelectedInsertListItemProperty, Nothing)
            autoInsertControl.SetValue(IsPopUpOpenProperty, False)
            ctl.Focus()
        End If
    End Sub



    Public ReadOnly Property VisibleItems As IEnumerable(Of IAutoInsertItem)
        Get
            Return CType(GetValue(VisibleItemsProperty), IEnumerable(Of IAutoInsertItem))
        End Get

        'Set(ByVal value As IEnumerable)
        '    SetValue(VisibleItemsProperty, value)
        'End Set
    End Property

    Public Shared ReadOnly VisibleItemsProperty As DependencyProperty =
                           DependencyProperty.Register("VisibleItems",
                           GetType(IEnumerable(Of IAutoInsertItem)), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))



    Public Property Placement As PlacementMode
        Get
            Return CType(GetValue(PlacementProperty), PlacementMode)
        End Get

        Set(ByVal value As PlacementMode)
            SetValue(PlacementProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PlacementProperty As DependencyProperty =
                           DependencyProperty.Register("Placement",
                           GetType(PlacementMode), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(PlacementMode.Relative))



    Public Property PlacementRectangle As Rect
        Get
            Return CType(GetValue(PlacementRectangleProperty), Rect)
        End Get

        Set(ByVal value As Rect)
            SetValue(PlacementRectangleProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PlacementRectangleProperty As DependencyProperty =
                           DependencyProperty.Register("PlacementRectangle",
                           GetType(Rect), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))


    Public Property HorizontalOffset As Double
        Get
            Return CDbl(GetValue(HorizontalOffsetProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(HorizontalOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HorizontalOffsetProperty As DependencyProperty =
                           DependencyProperty.Register("HorizontalOffset",
                           GetType(Double), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Double.Parse("0")))


    Public Property VerticalOffset As Double
        Get
            Return CDbl(GetValue(VerticalOffsetProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(VerticalOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly VerticalOffsetProperty As DependencyProperty =
                           DependencyProperty.Register("VerticalOffset",
                           GetType(Double), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Double.Parse("20")))



    Public Property Popupanimation As PopupAnimation
        Get
            Return CType(GetValue(PopupanimationProperty), PopupAnimation)
        End Get

        Set(ByVal value As PopupAnimation)
            SetValue(PopupanimationProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PopupanimationProperty As DependencyProperty =
                           DependencyProperty.Register("Popupanimation",
                           GetType(PopupAnimation), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(PopupAnimation.Slide))



    Public Property StaysOpen As Boolean
        Get
            Return CBool(GetValue(StaysOpenProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(StaysOpenProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StaysOpenProperty As DependencyProperty =
                           DependencyProperty.Register("StaysOpen",
                           GetType(Boolean), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(False))



    Public Property ListItemTemplate As DataTemplate
        Get
            Return CType(GetValue(ListItemTemplateProperty), DataTemplate)
        End Get

        Set(ByVal value As DataTemplate)
            SetValue(ListItemTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ListItemTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("ListItemTemplate",
                           GetType(DataTemplate), GetType(AutoInsertPopupControl), New PropertyMetadata(Nothing, AddressOf ListItemTemplateProperty_Changed))
    Private Shared Sub ListItemTemplateProperty_Changed(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

    End Sub


    Public Property HeaderTemplate As ControlTemplate
        Get
            Return CType(GetValue(HeaderTemplateProperty), ControlTemplate)
        End Get

        Set(ByVal value As ControlTemplate)
            SetValue(HeaderTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("HeaderTemplate",
                           GetType(ControlTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))




    Public Property Header As Object
        Get
            Return GetValue(HeaderProperty)
        End Get

        Set(ByVal value As Object)
            SetValue(HeaderProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderProperty As DependencyProperty =
                           DependencyProperty.Register("Header",
                           GetType(Object), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))



    Public Property FooterTemplate As ControlTemplate
        Get
            Return CType(GetValue(FooterTemplateProperty), ControlTemplate)
        End Get

        Set(ByVal value As ControlTemplate)
            SetValue(FooterTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FooterTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("FooterTemplate",
                           GetType(ControlTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))


    Public Property Footer As Object
        Get
            Return GetValue(FooterProperty)
        End Get

        Set(ByVal value As Object)
            SetValue(FooterProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FooterProperty As DependencyProperty =
                           DependencyProperty.Register("Footer",
                           GetType(Object), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))



#End Region


    Private _seletItemCommand As ICommand
    Public Property SeletItemCommand() As ICommand
        Get
            If _seletItemCommand Is Nothing Then _seletItemCommand = New RelayCommand(AddressOf ChooseContentCommand_Execute, AddressOf ChooseContentCommand_CanExecute)
            Return _seletItemCommand
        End Get
        Set(ByVal value As ICommand)
            _seletItemCommand = value
        End Set
    End Property

    Private Sub ChooseContentCommand_Execute(obj As Object)
        SetValue(SelectedInsertListItemProperty, obj)
        Dim seletedEvent As New RoutedEventArgs(AutoInsertPopupControl.ItemSelectedEvent)
        MyBase.RaiseEvent(seletedEvent)
    End Sub

    Private Function ChooseContentCommand_CanExecute(obj As Object) As Boolean
        Return obj IsNot Nothing
    End Function



    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        _popupBorder = CType(Template.FindName("PART_PopupBorder", Me), Border)
        _listBox = CType(Template.FindName("PART_ListBox", Me), ListBox)
        _popUp = CType(Template.FindName("PART_Popup", Me), Popup)

        'ListItemTemplate = CType(FindResource("DefaultListItemTemplate"), DataTemplate)

    End Sub



End Class
