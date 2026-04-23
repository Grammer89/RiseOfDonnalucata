INCLUDE globalVariables.ink

    { missione:
       -0: ->intro
       -1 :->mission
       -2 :->completed 
       }
       

===intro===
-Benvenuto a Neapolis!

->END

===mission===

mi accompagneresti a casa?
+[Si] ->si
+[No] ->no
->END

===si===
    -andiamo...eheheh
    ~missione = missione + 1
    ->END  
===no===
 -Munnezz
 ->END  
 
===completed===
~MissionCompleted = "X"
-grazie...vieni a prendere un caffè...
-Rifacciamolo...      
->END
            