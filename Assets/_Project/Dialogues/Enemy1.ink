INCLUDE GlobalVariables.ink

 { missione:
       -0: ->intro
       -1 :->intro
       -2 :->intro 
       -3 : -> intro
       -4 : -> fight
       -5 : -> completed
       }
       
===intro===

-ehehe
->END

===fight===
-tu vuoi sconfiggere me? Panzonee!
~missione = missione + 1
~ CanIfight = "X"
->END
===completed===
-Sei forte Panzone, ti vuoi alleare con noi?
->END