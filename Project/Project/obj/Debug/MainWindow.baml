   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	   1clr-namespace:Project.AppDataProject.AppData  6-       Project.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Project     5   	   1appdataclr-namespace:Project.AppData     6-     � Title$1  +Проектная деятельность��5   	   $	��600��6(   $	��800��65   $	��600��6A   $	��800��6Q    � WindowState$ 	Maximized=�6`   .��5
      �  � RowDefinitions 5   
   �� 5      $	��50* �6   6+   �� 5      $
��200* �6   6,   .+�5   
   � $��0��6   ���#ffffff � ColumnDefinitions 5      �� 5      $
��0.3* �6#   62   �� 5      $
��0.2* �6#   62   �� 5      $
��0.2* �6#   62   �� 5      $
��0.2* �6#   62    5      �� 5      $
��100* �6    60   �� 5      $
��100* �6    60   �� 5      $	��10* �6    6/   .+�5      �� $��2��6   $��4��6"   $��5q�66   ���#002fa76b   �� 5      $��0��6   $��0��6$   $��2��61   $
��Left=�6B   $��Resources\logo3.jpg��6]   6|   �� 5"      -   5         ��Name$ 
btnProject��5"   G   $y�Visible=�6   $��1��6*   $��0��6:      	btnHeader#��[  6[   $��ПРОЕКТЫ��6�   6�   �� 5#      -   5        $ btnExit��5#   G   $y�Visible=�6   $��2��6*   $��0��6:   #��[  6X   $��
ВЫХОД��6�   6�   �� 5%      -   5        $��btnPrsnlAcc��5%   u   $y�Visible=�6   $��3��6*   $��0��6:   $��2��6G   $��Center=�6X    � Cursor$
 Hand��6�   ���#ffffff$��0q�5&   *   $	��175��6>   $��75��6J   $��Stretch=�6V   $��Stretch=�6{   .��5'      � $��Stretch=�6   $��Stretch=�65    5(      �� 5)      $��0.25* �6+   6;   �� 5*      $
��0.5* �6+   6:    5,      �� 5-      $
��100* �6(   68   �� 5.      $
��100* �6(   68   .+�51      �� -   5        $��ImageString��51   =   $��0��6    $��0��60   $y�Visible=�6P   $��1��6e   $��1��6o   6|   A� 52      $��0��6   $��0��6.   $��2��6;   $��50��6L   $��50��6X   ��53      �� 54       j� Stretch$ UniformToFill=�6)    �� ImageSource 6A   �� *Text+ �� ElementName$ ImageString��56      �� 58      -   5        $��	txtbxName��58   X   $��1��6    $��0��60   $��Bottom=�6=     txtblHeader#��[ 6i   6�   �� 59      -   5        $��txtbxSurname��59   U   $��1��6    $��1��60   $	��Top=�6=   #��[ 6i   6�   5:      5;      5<      �� 5>   
   $��1��6   $��Center=�6"   $��Center=�6?   .+�5?      �� $��Resources\pattern.png��6   	 J� Opacity$	 .5Q�63   6B   5@       � 5A   
   -   5        
  �Name$
 FrmMain��5A      $��1��6     � NavigationUIVisibility$ Hidden=�6.   6P   5C      5D      