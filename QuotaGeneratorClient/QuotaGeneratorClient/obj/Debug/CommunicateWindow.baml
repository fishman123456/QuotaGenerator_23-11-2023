   M S B A M L   `   `   `  ����O  KQuotaGeneratorClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:QuotaGeneratorClientQuotaGeneratorClient  5   	   ,    &QuotaGeneratorClient.CommunicateWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:QuotaGeneratorClient     5   	   -   5          � Title$  CommunicateWindow��5   	   $	��150��6#   $	��660��60   $��20(�6<   .��5	      ��  �� Orientation$ Vertical=�6   .+�5   
   �� $ 
Horizontal=�6   .+�5      �� $��Server Command:��6   $��10q�6.   $	��200��6:   �� 5      -   5         ��Name$ serverCommandTextBox��5      $��10q�64   $
��ping��6@   $	��300��6L   �� 5      -   5         ��Name$ sendCommandButton��5      $
��Send��60   $��10q�6?   $	��100��6K   6W   5      �� 5   
   $ 
Horizontal=�6   .+�5      �  � ColumnDefinitions 5      �� 5      $
��100* �6'   66   �� 5      $
��100* �6'   66   �� 5      $
��100* �6'   66   �� 5      $
��100* �6'   66   .+�5      �� $��Result:��6   $��10q�6*   $��0��66   �� 5      -   5        $ resultTextBox��5      $��1��61   $��2��6B   $��10q�6W   $	��400��6c   5      5      5      5      