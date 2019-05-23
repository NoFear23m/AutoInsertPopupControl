# Was ist das AutoInsertPopupControl?

Es ist ein WPF CustomControl welches so flexibel wie möglich einsetzbar sein soll und im Grunde alles abdecken soll was man von einem Popup-Control erwartet. Die Grundidee war ein Control für "Textvorlagen". Als wie eine Autovervollständigung. Weiter ging es mit der Idee es auch wie eine Art Itellisense zu nutzen. Aber nicht nur mit reinem Text sondern mit der Optic welche der Entwickler möchte weshalb alle Teile den Controls mittels Templates implementiert wurden. Das Control ist also voll anpassbar.

# Wie funktioniert es?

Das Control wird mittels der Eigenschaft `TargetControl` an eine TextBox bzw. an jedes Control welches von TextBox erbt gebunden. Von nun an kann das Control mit dieser Textbox bereits verwendet werden.

     <TextBox x:Name="txtTest1" Width="500" HorizontalAlignment="Left" Height="100" TextWrapping="WrapWithOverflow" 
                         VerticalScrollBarVisibility="Auto" HorizontalScrollBarVisibility="Disabled" AcceptsReturn="True"/>

    <AutoInsertPopup:AutoInsertPopupControl x:Name="pop1" TargetControl="{Binding ElementName=txtTest1}" OpenOnTargetFocused="True"
                                                AutoInsertList="{Binding TestList}" OpenPopupTriggerChar="#" ReplaceTriggerChar="True"
                                                VerticalOffset="{Binding ElementName=txtTest1, Path=ActualHeight}"/>
Nähere Informationen findet Ihr im Wiki

## On Nuget?

Ja, Ihr findet immer das aktuelle Release auf Nuget.org.
https://www.nuget.org/packages/AutoInsertPopup

> Install-Package AutoInsertPopup -Version 1.0.1

##Screenshots (vorläufige)

Findet ihr im Wiki
