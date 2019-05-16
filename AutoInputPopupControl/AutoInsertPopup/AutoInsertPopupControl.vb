
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Controls.Primitives

<TemplatePart(Name:="PART_ListBox", Type:=GetType(ListBox))>
<TemplatePart(Name:="PART_PopupBorder", Type:=GetType(Border))>
<TemplatePart(Name:="PART_Popup", Type:=GetType(Popup))>
Public Class AutoInsertPopupControl
    Inherits Control


    Public Shared ReadOnly ItemSelectedEvent As RoutedEvent


    Private _listBox As ListBox


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

    <Description("Ruft die TextBox ab auf welche das Popup angewendet werden soll bzw. legt diese fest"), Category("Popup-Options")>
    Public Property TargetControl As TextBox
        Get
            Return CType(GetValue(TargetControlProperty), TextBox)
        End Get

        Set(ByVal value As TextBox)
            SetValue(TargetControlProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TargetControlProperty As DependencyProperty =
                           DependencyProperty.Register("TargetControl",
                           GetType(TextBox), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing, AddressOf TargetControlProperty_Changed))

    Private Shared Sub TargetControlProperty_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If e.NewValue IsNot Nothing Then
            Dim tb = DirectCast(e.NewValue, TextBox)
            tb.Tag = d
            AddHandler tb.PreviewKeyDown, AddressOf TargetControlProperty_KeyDown
            AddHandler tb.PreviewKeyUp, AddressOf TargetControlProperty_KeyUp
            Dim ctl = DirectCast(d, AutoInsertPopupControl)
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
        Dim lb = TryCast(DirectCast(sender, TextBox).Tag, AutoInsertPopupControl)
        If lb Is Nothing OrElse lb.GetValue(OpenPopupTriggerCharProperty) Is Nothing Then Exit Sub

        If DirectCast(sender, TextBox).CaretIndex = 0 OrElse e.Key = DirectCast(lb.GetValue(ClosePopupKeyProperty), Key) Then
            lb.isRecording = False : lb.recordStartPosition = 0
            lb.SetValue(IsPopUpOpenProperty, False)
            Exit Sub
        End If

        Dim lastChar As Char = CChar(DirectCast(sender, TextBox).Text.Substring(DirectCast(sender, TextBox).CaretIndex - 1, 1))
        If lb.GetValue(OpenPopupTriggerCharProperty).ToString().Contains(lastChar) Then
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
                            IEnumerable(Of IAutoInsertItem)).OrderBy(Function(o) o.SortingIndex).ThenBy(Function(oo) oo.SearchStringContent) _
                            .Where(Function(x) x.SearchStringContent.ToLower.Contains(lb.currentFilterString.ToLower))
                            )
                Else
                    If TryCast(fullList, List(Of String)) Is Nothing Then
                        Throw New Exception("The collection must be an IAutoInsertItem-Collection or a collection of string")
                    End If

                    Dim list = New List(Of IAutoInsertItem)
                    DirectCast(fullList, IEnumerable(Of String)).ToList.ForEach(Sub(x) list.Add(New AutoInsertItem(x)))
                    lb.SetValue(VisibleItemsProperty, list.OrderBy(Function(o) o.SortingIndex).ThenBy(Function(oo) oo.SearchStringContent) _
                                .Where(Function(x) x.SearchStringContent.ToLower.Contains(lb.currentFilterString.ToLower))
                                )
                End If

            End If
            If e.Key = Key.Tab AndAlso Boolean.Parse(lb.GetValue(InsertFirstListItemWithTabProperty).ToString()) Then
                lb.ChooseContentCommand_Execute(DirectCast(lb.GetValue(VisibleItemsProperty), IEnumerable(Of IAutoInsertItem)).FirstOrDefault)
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


    <Description("Ruft das Zeichen ab welches getippt werden muss um das Popup zu öffnen bzw. legt das Zeichen fest"), Category("Popup-Options")>
    Public Property OpenPopupTriggerChar As String
        Get
            Return CType(GetValue(OpenPopupTriggerCharProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(OpenPopupTriggerCharProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OpenPopupTriggerCharProperty As DependencyProperty =
                           DependencyProperty.Register("OpenPopupTriggerChar",
                           GetType(String), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))

    <Description("Ruft ab ob das auslösezeichen (PopupTriggerchar) nach dem einfügen entfernt werden soll bzw. legt diese fest"), Category("Popup-Options")>
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


    <Description("Ruft ab ob das erste Item der Ergebnisliste direkt mit TAB übernomen werden kann bzw. legt den Wert fest"), Category("Popup-Options")>
    Public Property InsertFirstListItemWithTab As Boolean
        Get
            Return CBool(GetValue(InsertFirstListItemWithTabProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(InsertFirstListItemWithTabProperty, value)
        End Set
    End Property

    Public Shared ReadOnly InsertFirstListItemWithTabProperty As DependencyProperty =
                           DependencyProperty.Register("InsertFirstListItemWithTab",
                           GetType(Boolean), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(True))


    <Description("Ruft den Key ab mit welchem der Focus auf das Popup gesetzt wird bzw. legt diesen fest"), Category("Popup-Options")>
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


    <Description("Ruft den Key ab mit welchem das Popup geschlossen werden kann bzw. legt diesen fest"), Category("Popup-Options")>
    Public Property ClosePopupKey As Key
        Get
            Return CType(GetValue(ClosePopupKeyProperty), Key)
        End Get

        Set(ByVal value As Key)
            SetValue(ClosePopupKeyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ClosePopupKeyProperty As DependencyProperty =
                           DependencyProperty.Register("ClosePopupKey",
                           GetType(Key), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Key.Escape))



    <Description("Ruft ab ob das Popup aktuell geöffnet ist bzw. legt den Wert fest"), Category("Popup-Options")>
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

    <Description("Ruft die Liste der verfügbaren Einträge ab bzw. legt diese fest"), Category("Einträge")>
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


    <Description("Ruft das in der Liste dr Einträge selektierte Item ab bzw. legt dieses fest"), Category("Einträge")>
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
            'Dim triggerChar = CChar(autoInsertControl.GetValue(OpenPopupTriggerCharProperty))
            Dim replaceTriggerChar As Boolean = CBool(autoInsertControl.GetValue(ReplaceTriggerCharProperty))

            Dim replaceTriggerString As String = ""
            If autoInsertControl.recordStartPosition < ctl.Text.Length Then
                replaceTriggerString = ctl.Text.Substring(autoInsertControl.recordStartPosition, 1)
            End If
            Dim replaceString = If(replaceTriggerChar, replaceTriggerString, "") & autoInsertControl.currentFilterString

            If replaceString.Length = 0 Then
                If autoInsertControl.isRecording Then
                    ctl.Text = ctl.Text.Insert(autoInsertControl.recordStartPosition, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
                Else
                    ctl.Text = ctl.Text.Insert(ctl.CaretIndex, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
                End If
                ctl.CaretIndex = autoInsertControl.recordStartPosition + DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString.Length
            Else
                If autoInsertControl.isRecording Then
                    ctl.Text = ctl.Text.Replace(replaceString, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
                    ctl.CaretIndex = autoInsertControl.recordStartPosition + DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString.Length
                Else
                    Dim lastCaretIndex = ctl.CaretIndex
                    ctl.Text = ctl.Text.Insert(lastCaretIndex, DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString)
                    ctl.CaretIndex = lastCaretIndex + DirectCast(e.NewValue, IAutoInsertItem).TextBoxInsertString.Length
                End If
            End If

            autoInsertControl.isRecording = False
            autoInsertControl.currentFilterString = ""

            If Not Boolean.Parse(autoInsertControl.GetValue(StaysOpenProperty).ToString) Then autoInsertControl.SetValue(IsPopUpOpenProperty, False)
            ctl.Focus()
        End If
    End Sub


    <Description("Ruft die aktuell sichbaren (gefiltert) Einträge der Auflistung ab"), Category("Einträge")>
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


    <Description("Ruft die TextBox ab auf welche das Popup angewendet werden soll bzw. legt diese fest"), Category("Popup-Options")>
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


    <Description("Ruft den Bereich in welchem sich das Popup befinden soll ab bzw. legt diesen fest"), Category("Popup-Options")>
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

    <Description("Ruft den Horizontalen Offset ab bzw. legt diesen fest"), Category("Popup-Options")>
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

    <Description("Ruft den vertikalen Offset ab bzw. legt diesen fest"), Category("Popup-Options")>
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


    <Description("Ruft ab wie das Popup beim einblenden Animiert werden soll bzw. legt die Animationart fest"), Category("Popup-Options")>
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


    <Description("gibt zurück ob das Popup nach erfolgreichem einfügen offen bleiben soll bzw. legt den Wert fest"), Category("Popup-Options")>
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


    <Description("Gibt zurück ob der Rahmen des Popups sichbar sein soll bzw. legt den Wert fest"), Category("Popup-Options")>
    Public Property PopupBorderVisibility As Visibility
        Get
            Return CType(GetValue(PopupBorderVisibilityProperty), Visibility)
        End Get

        Set(ByVal value As Visibility)
            SetValue(PopupBorderVisibilityProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PopupBorderVisibilityProperty As DependencyProperty =
                           DependencyProperty.Register("PopupBorderVisibility",
                           GetType(Visibility), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Visibility.Visible))

    <Description("Gibt die breite des Popuprahmens zurück bzw. legt die breite fest"), Category("Popup-Options")>
    Public Property PopupBorderThickness As Thickness
        Get
            Return CType(GetValue(PopupBorderThicknessProperty), Thickness)
        End Get

        Set(ByVal value As Thickness)
            SetValue(PopupBorderThicknessProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PopupBorderThicknessProperty As DependencyProperty =
                           DependencyProperty.Register("PopupBorderThickness",
                           GetType(Thickness), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(New Thickness(1)))


    <Description("Gibt den Brush für den Popuprahmen zurück bzw. legt den Wert fest"), Category("Popup-Options")>
    Public Property PopupBorderBrush As Brush
        Get
            Return CType(GetValue(PopupBorderBrushProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(PopupBorderBrushProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PopupBorderBrushProperty As DependencyProperty =
                           DependencyProperty.Register("PopupBorderBrush",
                           GetType(Brush), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(New SolidColorBrush(Colors.Black)))




    <Description("Ruft das Template des ListItems ab bzw. legt dieses fest"), Category("Popup-Options")>
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


    <Description("Ruft das Template des ListBoxItemsPanel ab bzw. legt dieses fest"), Category("Popup-Options")>
    Public Property ListBoxItemsPanelTemplate As ItemsPanelTemplate
        Get
            Return CType(GetValue(ListBoxItemsPanelTemplateProperty), ItemsPanelTemplate)
        End Get

        Set(ByVal value As ItemsPanelTemplate)
            SetValue(ListBoxItemsPanelTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ListBoxItemsPanelTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("ListBoxItemsPanelTemplate",
                           GetType(ItemsPanelTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))




    <Description("Ruft das Template des Headers (Kopf) ab bzw. legt dieses fest"), Category("Popup-Options")>
    Public Property HeaderTemplate As DataTemplate
        Get
            Return CType(GetValue(HeaderTemplateProperty), DataTemplate)
        End Get

        Set(ByVal value As DataTemplate)
            SetValue(HeaderTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("HeaderTemplate",
                           GetType(DataTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))



    <Description("Ruft den Inhalt des Headers (Kopf) ab bzw. legt diesen fest"), Category("Popup-Options")>
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


    <Description("Ruft das Template des Fußes ab bzw. legt dieses fest"), Category("Popup-Options")>
    Public Property FooterTemplate As DataTemplate
        Get
            Return CType(GetValue(FooterTemplateProperty), DataTemplate)
        End Get

        Set(ByVal value As DataTemplate)
            SetValue(FooterTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FooterTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("FooterTemplate",
                           GetType(DataTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))

    <Description("Ruft den Inhalt des Foters (Fußes) ab bzw. legt diesen fest"), Category("Popup-Options")>
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



    <Description("Ruft das Template des Bereichs ab der angezeigt wird wenn es keine Ergebnisse in der Auflistung gibt bzw. legt dieses fest"), Category("Popup-Options")>
    Public Property NoFilterResultsContentTemplate As DataTemplate
        Get
            Return CType(GetValue(NoFilterResultsContentTemplateProperty), DataTemplate)
        End Get

        Set(ByVal value As DataTemplate)
            SetValue(NoFilterResultsContentTemplateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NoFilterResultsContentTemplateProperty As DependencyProperty =
                           DependencyProperty.Register("NoFilterResultsContentTemplate",
                           GetType(DataTemplate), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))


#End Region


    Private _selectItemCommand As ICommand
    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
    Public Property SelectItemCommand() As ICommand
        Get
            If _selectItemCommand Is Nothing Then _selectItemCommand = New RelayCommand(AddressOf ChooseContentCommand_Execute, AddressOf ChooseContentCommand_CanExecute)
            Return _selectItemCommand
        End Get
        Set(ByVal value As ICommand)
            _selectItemCommand = value
        End Set
    End Property



    Private Sub ChooseContentCommand_Execute(obj As Object)
        SetValue(SelectedInsertListItemProperty, obj)
        Dim seletedEvent As New RoutedEventArgs(AutoInsertPopupControl.ItemSelectedEvent)
        MyBase.RaiseEvent(seletedEvent)
        ItemSelectedCommand?.Execute(obj)
        SetValue(SelectedInsertListItemProperty, Nothing)
    End Sub

    Private Function ChooseContentCommand_CanExecute(obj As Object) As Boolean
        Return obj IsNot Nothing
    End Function



    <Description("Ruft den Command ab welche ausgelöst wird wenn ein Item selektiert (in die Textbox eingefügt) wurde bzw. legt diesen fest"), Category("Popup-Options")>
    Public Property ItemSelectedCommand As ICommand
        Get
            Return CType(GetValue(ItemSelectedCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(ItemSelectedCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ItemSelectedCommandProperty As DependencyProperty =
                           DependencyProperty.Register("ItemSelectedCommand",
                           GetType(ICommand), GetType(AutoInsertPopupControl),
                           New PropertyMetadata(Nothing))




    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        '_popupBorder = CType(Template.FindName("PART_PopupBorder", Me), Border)
        _listBox = CType(Template.FindName("PART_ListBox", Me), ListBox)
        '_popUp = CType(Template.FindName("PART_Popup", Me), Popup)

        'ListItemTemplate = CType(FindResource("DefaultListItemTemplate"), DataTemplate)

    End Sub



End Class
