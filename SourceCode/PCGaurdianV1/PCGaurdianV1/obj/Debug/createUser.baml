   M S B A M L   `   `   `  ����G  CPCGaurdianV1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PCGaurdianV1PCGaurdianV1  5          PCGaurdianV1.createUser   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PCGaurdianV1     5        N� Title$  
createUser��5	      .��5      � .+�5   
   �� $��Center=�6   $��Center=�62   .+�5      �� $
��Left=�6   $��42��63   $��0,0,0,0q�6?   $
��Wrap=�6P   $��Enter New User Details��6d   $	��Top=�6�   $	��419��6�   $��36(�6�    �� TextAlignment$ Center=�6�   �� 5      $��Center=�6   $��Center=�66   .+�5      � .+�5      �� -   5        $��	unameName��5      $��	User Name��6-   $
��Left=�6A   $��39��6\   $��10,10,10,10q�6h   $	��Top=�6}   $	��170��6�    J� RenderTransformOrigin$ 0.531,1.602&�6�   $��20(�6�   �� 5      -   5        $��unametxt��5      $
��Left=�6/   $��30��6J   $��10,10,10,10q�6V    �� 	MaxLength$ 30��6k    �� TextWrapping$
 Wrap=�6z   $�� ��6�   $	��230��6�    �� TextAlignment$ Center=�6�   $��15(�6�   5      �� 5      -   5        $��nomatch��5      $y�	Collapsed=�6,   
���  ��$��Right=�6T   $��20��6p   $
��Wrap=�6|   $��*Passwords did not match��6�   $	��238��6�   6�   �� 5      -   5        $��exist��5      $y�	Collapsed=�6*   
���  ��$��Right=�6R   $��20��6n   $
��Wrap=�6z   $��Username not available��6�   $	��238��6�   6�   �� 5      -   5        $��blkuname��5      $y�	Collapsed=�6-   
���  ��$��Right=�6U   $��20��6q   $
��Wrap=�6}   $-��'*You can not leave username field blank��6�   $	��238��6�   6�   �� 5      -   5        $��blkpass��5      $y�	Collapsed=�6,   
���  ��$��Right=�6T   $��20��6p   $
��Wrap=�6|   $-��'*You can not leave password field blank��6�   $	��238��6�   6�   � 5      .+�5      �� -   5        $
��pass��5      $��Create Password��6(   $
��Left=�6B   $��39��6]   $��10,10,10,10q�6i   $	��Top=�6~   $	��170��6�   $ 0.531,1.602&�6�   $��20(�6�   G� 5      -   5        $��passtxt��5   "   $
��Left=�61   $��30��6L   $��10,10,10,10q�6X   $	��230��6m    G� 	MaxLength$ 30��6y   $��15(�6�   5      � 5      .+�5      �� -	   5        $��cpass��5      $��Confirm Password��6)   $
��Left=�6D   $��39��6_   $��10,10,10,10q�6k   $	��Top=�6�   $	��170��6�   $ 0.531,1.602&�6�   $��20(�6�   G� 5      -
   5        $��cpasstxt��5   "   $
��Left=�62   $��30��6M   $��10,10,10,10q�6Y   $	��230��6n   $ 30��6z   $��15(�6�   6�   5      � 5      $��Center=�6   .+�5       �� -   5        $��newUser��5       $��Create User��6,   $��Center=�6B   $��47��6_   $��0,0,0,0q�6k   $	��Top=�6|   $	��165��6�   $��20(�6�   6�   �� 5!      -   5        $
��Back��5!      $
��Back��6)   $��Center=�68   $��47��6U   $��20,0,0,0q�6a   $	��Top=�6s   $	��166��6�   $��20(�6�   6�   5"      5#      5$      5%      5&      