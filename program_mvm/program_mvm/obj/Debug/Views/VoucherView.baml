   M S B A M L   `   `   `  ����F  Bprogram_mvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:program_mvm.Viewsprogram_mvm.Views  5   	   #    program_mvm.Views.VoucherView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:program_mvm.Views     5   	     � Title$  VoucherView��5   
   $	��600��6   $	��800��6-   $	��600��5	   	   $	��550��6   ���#e8827b � WindowStartupLocation$ CenterScreen=�5   	   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� -   5         m�Name$ GoToTransactionMenu��5      $��Transaction Menu��68   m� 5      -   5        $ GoToEmployeeMenu��5      $��Employee Management Menu��65   m� 5      -   5        $ GoToProductMenu��5      $��Product Management Menu��64   m� 5      -   5        $ GoToVoucherMenu��5      $��Voucher Menu��64   m� 5      -   5        $ GoToReportingMenu��5      $��Reporting Menu��66   m� 5      -   5        $ GoToCustomerMenu��5      $��Customer Menu��65   5      5      � 5   
    � RowDefinitions 5      �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $��* �6    6-    � ColumnDefinitions 5      �� 5      .+�5!      � $��0��6   $��0��6#   $	��100��60    5"      �� 5#      $
��auto �6%   65   �� 5$      $
��auto �6$   64   �� 5%      $
��auto �6$   64   �� 5&      $
��auto �6$   64   �� 5'      $
��auto �6$   64   �� 5(      $
��auto �6$   64   �� 5)      $
��auto �6$   64   �� 5*      $
��auto �6$   64   �� 5+      $
��auto �6$   64   .+�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ VoucherList��5-         SelectionUnit$ FullRow=�60   $
��Auto=�6I   $
��Auto=�5.      $	��200��5/      $	��500��6-      
IsReadOnly . $	��200��50         AutoGenerateColumns .  	 �� SelectedValue	 51      �� 
 �� Path$
 SelectedRow�   Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ 	VoucherID��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� $
 	VoucherID�6e     56      $ VoucherDiscount��6-    6F   �� $
 VoucherDiscount�6q     57      $ 	IsApplied��6-    6@   �� $
 	IsApplied�6e   5:      5;      � 5<      $��1��6   $��0��6     5=      �� 5>      6)   �� 5?      6)    5A      �� 5B      6&   .+�5D      � $��0��6   $��0��6'    5E      �� 5F      $
��auto �6+   6;   �� 5G      $
��auto �6+   6;   �� 5H      $	��17* �6+   �� 5I      $
��130* �6+    5K      �� 5L      $
��auto �6(   69   �� 5M      $
��auto �6(   69   �� 5N      $
��auto �6(   69   �� 5O      $
��auto �6(   69   �� 5P      $
��auto �6(   69   �� 5Q      $
��auto �6(   69   �� 5R      $
��auto �6(   69   �� 5S      $
��auto �6(   69   �� 5T      $
��auto �6(   69   �� 5U      $
��auto �6(   69   .+�5Y      �� $��1��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   VoucherDiscount(in %)6w   �� 5Z      -   5         ��Name$ VoucherDiscount��5Z      $��1��69   $��1��6F   $	��200��6V   $
��Left=�6e   $	��201��6�   $��2��6�   $��1,0,0,0q�6�   6�   �� 5[      $��2��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   	IsApplied6k   �� 5\      $��2��6!   $��1��6.   .+�5]      ��  �� SelectedItem 6#   �� $
 	IsApplied�.@�5^      �� .��6,   
NotApplied68   �� 5_      .��6+   	IsApplied66   5`      5a      5b      5d      � 5e      $��2��6   $��0��6    $��20��62   $��Bottom=�6>    5f      �� 5g      6)   �� 5h      6)   �� 5i      6)    5k      �� 5l      6&   .+�5n      �� -	   5         ��Name$ DoAddVoucher��5n      $��0��6/   $��0��6<   $	��Add��6L   $��20��6Z   $	��Top=�6f   6�   �� 5o      -
   5        $ DoUpdateVoucher��5o      $��0��62   $��1��6?   $��Update��6O   6b   �� 5p      -   5        $ DoDeleteVoucher��5p      $��0��63   $��2��6@   $��Delete��6P   6c   5q      5r      5s      5u      