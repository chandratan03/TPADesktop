   M S B A M L   `   `   `  ����F  Bprogram_mvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:program_mvm.Viewsprogram_mvm.Views  5   	   !    program_mvm.Views.StockView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:program_mvm.Views     5   	     � Title$  	StockView��5   	   $	��500��6   $	��600��6+   $	��600��5	   	   $	��550��6   ���#e8827b � WindowStartupLocation$ CenterScreen=�5   	   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Stocks��5         SelectionUnit$ FullRow=�6#   $
��Auto=�6<   $
��Auto=�5         
IsReadOnly .    AutoGenerateColumns .   �� SelectedValue 5      ��  �� Path$ SelectedRow�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 StockID��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 66   �� $ StockID�6Y     5      $	 	ProductID��6%   
 68   �� $ 	ProductID�6]     5      $	 ManyIn��6%   
 65   �� $ ManyIn�6W     5      $	 ManyOut��6%   
 66   �� $ ManyOut�6Y     5      $	 RewardStocks��6%   
 6;   �� $ RewardStocks�6c     5      $	 BrokenStocks��6%   
 6;   �� $ BrokenStocks�6c     5      $	 AvailableStocks��6%   
 6>   �� $ AvailableStocks�6i     5      $
	 Date��6%   
 63   �� $
 Date�6T     5      $	 Stock point��6%   
 6:   �� $ Product.RestockPoint�6k   5      5      5       