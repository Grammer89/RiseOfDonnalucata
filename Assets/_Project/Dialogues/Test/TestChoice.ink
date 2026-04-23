INCLUDE globals.ink
//Verifica se è stato selezionato già qualcosa va su already chose
//altrimenti richiamo il nodo main
{ pornattrice == "": ->main | ->already_chose}

===main===
Which PornStar do you like?
      + [Selen]
        ->chosen("Selen")
      + [Milly d'Abbraccio]
        ->chosen("Milly d'Abbraccio")
      + [Silvia Taylor]
        ->chosen("Silvia Taylor")
        
===chosen(PornStar)===
~ pornattrice = PornStar
You are chose {pornattrice}
->END


===already_chose ===
You already chose {pornattrice}!
->END