   M S B A M L   `   `   `  ����N  JCrossMachineManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CrossMachineManagerCrossMachineManager  5   	   $    CrossMachineManager.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:CrossMachineManager     5   	   -   5          �Name$  MyWindow��5   	    � Title$$ Cross Platform Machine Manager��5   	   $	��500��60   $
��1000��6=   .��5
      � .+�5   
   �� -   5         ��Name$ OpenList��5      $��Open Machine List��6#   $
��Left=�6?   $��	19,10,0,0q�6Z   $	��Top=�6m   $	��114��6�   6�   �� 5   
   -   5        $ SaveResults��5      $��Save Results��6&   $
��Left=�6=   $��	19,35,0,0q�6X   $	��Top=�6k   $	��114��6�   6�   �� 5   
   -   5         ��Name$ 	lbcommand��5      $��Command:��6#   $
��Left=�66   $��	19,60,0,0q�6Q   $	��Top=�6d   $��70��6|   �� 5   
   -   5         ��Name$ CommandTextBox��5      $
��Left=�6*   $��23��6E   $��	94,60,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��407��6�   �� 5   
   -   5        $ 	RunButton��5      $	��Run��6$   $
��Left=�62   $��
520,60,0,0q�6M   $	��Top=�6a   $��75��6y   $��23��6�   �� 5   
   -   5        $ lbuser��5      $��User:��6    $
��Left=�60   $��	156,7,0,0q�6K   $	��Top=�6^    J� RenderTransformOrigin$ -0.026,0.538&�6v   $��48��6�   �� 5   
   -   5        $ 
lbpassword��5      $��	Password:��6$   $
��Left=�68   $��
154,32,0,0q�6S   $	��Top=�6g   �� 5   
   -	   5        $ UserTextBox��5      $
��Left=�6'   $��23��6B   $��	221,7,0,0q�6N   $
 Wrap=�6a   $	��Top=�6u   $	��136��6�   �� 5   
   -
   5        $ PasswordTextBox��5      $
��Left=�6+   $��23��6F   $��
221,35,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��136��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ VMList��5      $
��Left=�6#   $
��Auto��6>   $��
19,100,0,0q�6L   $	��Top=�6`   $
��Auto��6x   $ 
1.75,2.231&�6�   6�   �� 5   
   -   5        $ uploadButton��5      $"��Upload file for all machines��6'   $
��Left=�6N   $��	387,7,0,0q�6i   $	��Top=�6|   $	��208��6�   6�   5      5      