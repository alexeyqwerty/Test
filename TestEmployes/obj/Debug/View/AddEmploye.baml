   M S B A M L   `   `   `  ����G  CTestEmployes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TestEmployes.ViewTestEmployes.View  5   	   A%clr-namespace:TestEmployes.View.UtilsTestEmployes.View.Utils  5   	   "    TestEmployes.View.AddEmploye   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:TestEmployes.View     5   	   7utils%clr-namespace:TestEmployes.View.Utils     5   	   
�������$��1q�5   	   
���   �  � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterOwner=�6    � 
ResizeMode$ NoResize=�5   	    � SizeToContent$ WidthAndHeight=�6   2   ,TestEmployes.View.Utils.DependencyProperties   DialogResult 5	   	   �� *DialogResult+.��5      � $��10q�6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��5q�5      $��0��5      ��5      �� *Title+5      �� 5!   
   $��Имя��6   $��1��5"      �� 5$   
   $��2��6   $��5q�5%      $	��300��5&      ��5'      �� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5)   
   $��Фамилия��6   $��3��5*      �� 5,   
   $��4��6   $��5q�5-      $	��300��5.      ��5/      �� *	Surname+$ TwoWay=�$ PropertyChanged=��� 51   
   $��Дата рождения��6   $��5��52      �� 54   
   $��6��6   $��5q�55      $	��300��56      ��57      �� *
BirthDay+$ TwoWay=�$ PropertyChanged=��� 59   
   $ ��Подразделение��6   $��7��5:      �� 5<   
   $��8��6    �� DisplayMemberPath$
 Name��5>      ��5=      �� *	Divisions+	 �� SelectedItem	 5?      �� *SelectedDivision+$ TwoWay=�$ PropertyChanged=�� 5A   
   $��9��6   $��Right=�5B      $��5q�5C      .+�5D      �� $��Сохранить��6   $��0 0 5 0q�5E      $��80��5G      ��5F      �� *Save+5G      �� 5H      $��Отмена��6   $��80��5J      ��5I      �� *Close+5J      5K      5L      5M      