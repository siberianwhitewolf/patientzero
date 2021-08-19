VAR contiene_item = 0 

- Una hierba verde
- Son una especie que proviene de las montañas del medio oeste.
- Si la aplico en una herida puede sanarla moderadamente.
- ¿Me la llevo?
-> OPCIONES

== OPCIONES ==
* [SI] -> SI
* [NO] -> NO

== SI ==
- Obtienes una hierba verde #hierba
-> END

== NO ==
-> END


=== function asignar_contiene_item (x)====
~ contiene_item = x