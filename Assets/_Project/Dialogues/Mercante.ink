INCLUDE GlobalVariables.ink

->intro 

===intro===
-We wajo, benvenuto a Neapolis!
vuoi comprare qualche bella pozioncina?
+[Si] ->Si
+[No] ->No
->END



===Si===
-Bell fratè
+[Compra] ->SetOpenItemShop

->END

===No===
-Non sai che ti perdi!
->END

===SetOpenItemShop===
~OpenItemShop = "X"
->END
