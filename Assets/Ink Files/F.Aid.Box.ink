VAR contiene_item = 0

-- Una caja de primeros auxilios
- Contiene algunas herramientas propias de la cirugía.

{ contiene_item > 0: 
-> HAY_ITEM
-else: 
-> NO_HAY_ITEM
}


==HAY_ITEM==
- Hay un tubo de ensayo.
- ¿Me lo llevo?

* [TOMAR] -> SI
* [DEJAR] -> NO

 == SI ==
    Obtienes un tubo de ensayo. #tubo
    ~ contiene_item = 0 
    -> END
    
    == NO ==
    Cierras la caja
    -> END

->END

==NO_HAY_ITEM==
- No hay nada de interes.
-> END

=== function asignar_contiene_item (x)====
~ contiene_item = x