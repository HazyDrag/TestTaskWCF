   M S B A M L   `   `   `  ����O  KAddressLibraryClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:AddressLibraryClientAddressLibraryClient  5   	   "    AddressLibraryClient.Library   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:AddressLibraryClient     5   	   -   5          �Name$  libraryWindow��5   	    � Title$) #Библиотека адресов��5   	   $��514.692��6$   $��769.066��65   $��Center=�6E   $��Center=�6b    � WindowStartupLocation$ CenterScreen=�6}   .��5	      � $��0,0,2,-1q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ libraryDataGrid��5
      $
��Left=�6,   $	��475��6G   $	��Top=�6T   $	��599��6l      HeadersVisibility$ Column=�6x      
IsReadOnly . 6�   �� 5   
   -   5         ��Name$ editAddressBlankButton��5      $.��(Бланк описания адреса��61   $
��Left=�6Q   $��
604,10,0,0q�6l   $	��Top=�6�   $	��145��6�   $��72��6�   6�   �� 5   
   -   5        $! descriptionBlankButton_Copy��5      $.��(Бланк описания адреса��66   $
��Left=�6V   $��
604,87,0,0q�6q   $	��Top=�6�   $	��145��6�   $��72��6�   6�   �� 5   
   -   5        $ printButton��5      $!��Печать записей��6&   $
��Left=�6?   $��604,164,0,0q�6Z   $	��Top=�6o   $	��145��6�   $��72��6�   6�   5      5      