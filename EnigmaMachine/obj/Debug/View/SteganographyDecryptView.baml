   M S B A M L   `   `   `  ����H  DEnigmaMachine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EnigmaMachine.ViewModelEnigmaMachine.ViewModel  5      1    +EnigmaMachine.View.SteganographyDecryptView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;	ViewModel%clr-namespace:EnigmaMachine.ViewModel     5        � Title$  Steganography Decrypter��5   	   $	��452��6)   $	��699��66    � WindowStartupLocation$ CenterScreen=�6B   $��Courier New)�6g    � 
ResizeMode$ CanMinimize=�6�   .��5      � .+�5   
   �� $��35��6   $��Steganography Decrypter��6   $��24(�6>   $	��Top=�6L   � 5   
   $��0 30 0 0q�6    � ColumnDefinitions 5	      �� 5
      $��2* �6#   �� 5      $��2* �6#   .+�5      �� $��0��6   $��10q�6)   .+�5      �� $��Image with hidden data��6   $
��Left=�69   $	��Top=�6U   $��24��6m   $	��312��6y   �� 5      $��30��6   ��6%   �� *LoadImageCommand+.��6J   
Load image6V   �� 5      $
��0 10q�6   $��Bottom=�6'   $��30��6B   ��6N   �� *ExtractDataCommand+.��6u   Extract data6�   �� 5      $��Extracted data��6   $
��Left=�61   $	��Top=�6M   $��24��6e   �� 5      $	��214��6   ���	#FFE2E2E2 �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�6y   ��6>   �� *
OutputText+ �� Mode$ OneWay=�6y   � 5      -   5        $��progressBar��5   �    � Minimum$ 0Q�6    � Maximum$	 100Q�6*   $��15��6�   $	��0,5q�6�   	 � IsIndeterminate	 68   �� *
Processing+
 � Value
 6_   �� *
Progress+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5      $��1��6   $��10q�6$   ��60   �� *	ImagePath+�� 5      $��1��6   $��50��6(   $��Bottom=�64   $
��Wrap=�6O   ��6c   �� *	ImagePath+5      5      5      