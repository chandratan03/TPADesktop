   M S B A M L   `   `   `  ����F  Bprogram_mvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:program_mvm.Viewsprogram_mvm.Views  5   	   '    !program_mvm.Views.TransactionView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:program_mvm.Views     5   	     � Title$  TransactionView��5   	   $	��600��6"   $	��800��61   $	��600��5	   	   $	��550��6   ���#e8827b � WindowStartupLocation$ CenterScreen=�5   	   .��5      ]� .+�5   
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
��auto �6$   64   .+�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ DetailTransactions��5-         SelectionUnit$ FullRow=�67   $
��Auto=�6P   $
��Auto=�5.      $	��200��5/      $	��500��6-      
IsReadOnly . $	��200��50         AutoGenerateColumns .  	 �� SelectedValue	 51      �� 
 �� Path$
 SelectedRow�   Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ Product Price��6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� $
 Product.ProductPrice�6t     56      $ ProductPrice��6X    6-   �� $
 Product.ProductID�6p     57      $ ProductName��6Z    6-   �� $
 Product.ProductName�6q     58      $ quantity��6O    6-   �� $
 quantity�6c   5<      5=      � 5>      $��1��6   $��0��6     5?      �� 5@      6)   �� 5A      6)    5C      �� 5D      6&   .+�5F      � $��0��6   $��0��6'    5G      �� 5H      $
��auto �6+   6;   �� 5I      $
��auto �6+   6;   �� 5J      $	��17* �6+   �� 5K      $
��130* �6+    5M      �� 5N      $
��auto �6(   69   �� 5O      $
��auto �6(   69   �� 5P      $
��auto �6(   69   �� 5Q      $
��auto �6(   69   �� 5R      $
��auto �6(   69   �� 5S      $
��auto �6(   69   �� 5T      $
��auto �6(   69   �� 5U      $
��auto �6(   69   �� 5V      $
��auto �6(   69   �� 5W      $
��auto �6(   69   .+�5Y      �� $��0��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   	ProductID6k   �� 5Z      -   5         ��Name$ 	ProductID��5Z      $��0��63   $��1��6@   $	��200��6P   $
��Left=�6_   $	��201��6z   $��2��6�   $��1,0,0,0q�6�   6�   �� 5[      $��1��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   ProductName6m   �� 5\      -	   5        $ ProductName��5\      $��1��63   $��1��6@   $	��200��6P   $��2��6_   $��1,0,32,0q�6s   6�   �� 5]      $��2��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   Status6h   �� 5^      -
   5        $ Status��5^      $��2��6.   $��1��6;   $	��200��6K   $��2��6Z   $��1,0,32,0q�6n   6�   �� 5_      $��3��6   $��0��6)   $��2��69   $��	0,0,200,0q�6M   .��6`   
Quantity6j   �� 5`      -   5        $	 Qty��5`      $��3��6+   $��1��68   $	��200��6H   $��2��6W   $��1,0,32,0q�6k   6   5a      �� 5b      $��1��6   $��1��6*   .+�5c      ��  �� 
IsReadOnly . 6   �� 5d      -   5         ��Name$ DoScanProduct��5d      .��65   	Scan Item6@   �� 5e      -   5        $ DoApplyVoucher��5e      .��65   Apply Voucher6D   �� 5f       �� Orientation$ 
Horizontal=�6!   .+�5g      �� .��6    Price (No discount):67   �� 5h      -   5         ��Name$ PriceNoDiscount��5h       .��69   06<   5i      �� 5j      $ 
Horizontal=�6"   .+�5k      �� .��6    Discount(%):6.   �� 5l      -   5        $ Discount��5l       .��62   065   5m      �� 5n      $ 
Horizontal=�6!   .+�5o      �� .��6    Price:6(   �� 5p      -   5        $ 
TotalPrice��5p       .��64   067   5q      �� 5r      -   5        $ DoRegisterMember��5r      .��67   Register Member6H   �� 5s      -   5        $ ShowBillMenu��5s      .��63   	PayBill6<   �� 5t      -   5        $" ShowDetailServiceTransaction��5t      .��6C   Show Bill Transaction6Z   �� 5u      -   5        $ RefreshPage��5u      .��62   Refresh Page6@   5v      5y      � 5z      $��2��6   $��0��6    $��20��62   $��Bottom=�6>    5{      �� 5|      6)   �� 5}      6)   �� 5~      6)   �� 5       5�      �� 5�      6&   .+�5�      �� -   5        $ DoaddDetailTransaction��5�      $��0��6:   $��0��6G   $	��Add��6W   6g   �� 5�      -   5        $ DoUpdateDetailTransaction��5�      $��0��6<   $��1��6I   $��Update��6Y   6l   �� 5�      -   5        $ DoDeleteDetailTransaction��5�      $��0��6<   $��2��6I   $��Delete��6Y   6l   �� 5�      -   5        $ 	DoPayment��5�      $��0��6,   $��3��69   $��Payment��6I   5�      5�      5�      5�      