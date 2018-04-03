using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PAET.Infraestructure;
using System.Collections.Generic;
using NSubstitute;
using System.Data.Entity;
using EMVS.Comun.DominioBase.PAET;

namespace PAET.Test
{
    [TestClass]
    public class PreguntasTest
    {
        [TestMethod]
        public void PreguntasServiceTest()
        {
            IQueryable<Preguntas> preguntas = new List<Preguntas>
            {
                new Preguntas{ IdPregunta = 1, IdTecnologia = 1, IdDificultad = 1 , IdValoracion = 1, IdTipoPregunta = 1, Descripcion ="¿Cómo se declara una variable en C#?",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 1, IdPregunta = 1, IdValoracion = 0, Descripcion = "Dim", Correcta = false },
                        new Respuestas { IdRespuesta = 2, IdPregunta = 1, IdValoracion = 2, Descripcion = "Tipo Implícito", Correcta = true },
                        new Respuestas { IdRespuesta = 3, IdPregunta = 1, IdValoracion = 3, Descripcion = "Declaraciones de tipos", Correcta = true },
                    }
                },
                new Preguntas{ IdPregunta = 2, IdTecnologia = 2, IdDificultad = 2 , IdValoracion = 3, IdTipoPregunta = 3, Descripcion ="Explica en breves palabras las diferencias entre una clase abstracta y una interfaz"},
                new Preguntas{ IdPregunta = 3, IdTecnologia = 1, IdDificultad = 3 , IdValoracion = 7, IdTipoPregunta = 2, Descripcion = "Escribe un programa que muestre en pantalla los números del 1 al 100, sustituyendo los múltiplos de 3 por la palabra “Fizz” y, a su vez, los múltiplos de 5 por “Buzz”. Para los guarismos que, al tiempo, son múltiplos de 3 y 5, utiliza el combinado “FizzBuzz”."},
                new Preguntas{ IdPregunta = 4, IdTecnologia = 2, IdDificultad = 3 , IdValoracion = 6, IdTipoPregunta = 3, Descripcion = "Explica brevemente los principios SOLID."},
                new Preguntas{ IdPregunta = 5, IdTecnologia = 4, IdDificultad = 2 , IdValoracion = 5, IdTipoPregunta = 3, Descripcion = "¿Para que sirve una clase declarada como Shared?."},
                new Preguntas{ IdPregunta = 6, IdTecnologia = 6, IdDificultad = 2 , IdValoracion = 4, IdTipoPregunta = 1, Descripcion = "Los métodos Skip y Take en Linq sirven para.....",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 4, IdPregunta = 6, IdValoracion = 0, Descripcion = "Take omite los primeros n elementos de una colección y coge el resto, mientras que Skip coge los elementos especificados de una colección", Correcta = false },
                        new Respuestas { IdRespuesta = 5, IdPregunta = 6, IdValoracion = 1, Descripcion = "Take coge los elementos especificados de una colección, mientras que Skip omite los primeros n elementos de una colección y coge el resto.", Correcta = true },
                        new Respuestas { IdRespuesta = 6, IdPregunta = 6, IdValoracion = 0, Descripcion = "Ninguna es correcta", Correcta = false },
                    }
                },
                new Preguntas{ IdPregunta = 7, IdTecnologia = 6, IdDificultad = 2 , IdValoracion = 3, IdTipoPregunta = 1, Descripcion = "¿Cómo ordenarías descendentemente una consulta en Linq por varios campos de una consulta?",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 7, IdPregunta = 7, IdValoracion = 1, Descripcion = "En la consulta LINQ, con orderby seguido del campo a ordenar y la palabra clave descending seguido de los siguientes campos separados por coma después de la misma palabra clave.", Correcta = false },
                        new Respuestas { IdRespuesta = 8, IdPregunta = 7, IdValoracion = 1, Descripcion = "Con orderbydescending indicando en una expresión lambda el campo por el que se quiere ordenar y utilizando thendescending y expresión lambda para cada campo adicional.", Correcta = false },
                        new Respuestas { IdRespuesta = 9, IdPregunta = 7, IdValoracion = 2, Descripcion = "Ambas son correctas.", Correcta = true },
                    }
                },
                new Preguntas{ IdPregunta = 8, IdTecnologia = 4, IdDificultad = 2 , IdValoracion = 2, IdTipoPregunta = 4, Descripcion = "¿Qué hace este código? ¿Dónde está el error?",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 10, IdPregunta = 8, IdValoracion = 2, Descripcion = "Public Class Vehicle ", Correcta = null}
                    }
                },
                new Preguntas{ IdPregunta = 9, IdTecnologia = 2, IdDificultad = 2 , IdValoracion = 2, IdTipoPregunta = 4, Descripcion = "¿Qué hace este código en C#?",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 11, IdPregunta = 9, IdValoracion = 2, Descripcion = "Public Class Vehicle ", Correcta = null}
                    }
                },
                new Preguntas{ IdPregunta = 10, IdTecnologia = 1, IdDificultad = 2 , IdValoracion = 2, IdTipoPregunta = 1, Descripcion = "public static Boolean IsNotNull(this Persona persona) ¿Qué hace?.",
                    Respuestas = new List<Respuestas> {
                        new Respuestas { IdRespuesta = 12, IdPregunta = 10, IdValoracion = 0, Descripcion = "Declara un método estático.", Correcta = false },
                        new Respuestas { IdRespuesta = 13, IdPregunta = 10, IdValoracion = 0, Descripcion = "Declara un método estático  que devuelve un boolean y que pasa como parámetro y objeto de tipo Persona.", Correcta = false },
                        new Respuestas { IdRespuesta = 14, IdPregunta = 10, IdValoracion = 2, Descripcion = "Declara un método estático  que devuelve un boolean y que extiende los objetos de tipo Persona.", Correcta = true },
                    }
                }
            }.AsQueryable();

            IDbSet<Preguntas> preguntasDbSet = Substitute.For<IDbSet<Preguntas>>();
            preguntasDbSet.Provider.Returns(preguntasDbSet.Provider);
            preguntasDbSet.Expression.Returns(preguntasDbSet.Expression);
            preguntasDbSet.ElementType.Returns(preguntasDbSet.ElementType);
            preguntasDbSet.GetEnumerator().Returns(preguntasDbSet.GetEnumerator());

            
        }
    }
}
