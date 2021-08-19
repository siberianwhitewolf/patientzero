VAR contiene_item = 0 

- Es un cajón.
- De su interior emana un aroma asfixiante.
- ¿Lo abro?
-> OPCIONES

== OPCIONES ==
* [ABRIR.] -> ABRIR
* [DEJAR.] -> DEJAR

== DEJAR ==
- No abres el cajón.
-> END

== ABRIR ==
- Está lleno de fármacos.
- Hiede a formol.


{ contiene_item > 0: 
-> HAY_HIERBA  
-else: 
-> NO_HAY_HIERBA 
}


    == HAY_HIERBA ==
    - Hay una hierba verde.
    - ¿Quieres tomarla?
    * [TOMAR] -> SI
    * [DEJAR] -> NO
    
    == NO_HAY_HIERBA ==
    -No hay nada de interés.
    -> END

    == SI ==
    Obtienes una hierba verde. #hierba
    ~ contiene_item = 0 
    -> END
    
    == NO ==
    Cierras el cajón.
    -> END
    
    
=== function asignar_contiene_item (x)====
~ contiene_item = x