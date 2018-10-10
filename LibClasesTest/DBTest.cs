﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibClases;
using System.Collections.Generic;

namespace LibClasesTest
{
    /// <summary>
    /// Descripción resumida de DBTest
    /// </summary>
    [TestClass]
    public class DBTest
    {
        private List<Encuesta> encuestas;
        private DB db;

        public DBTest()
        {
            encuestas = new List<Encuesta>();
            Usuario us = new Usuario("Dios", "QuienComoDios");
            db = DB.getTestDB(us);
            Encuesta en = new Encuesta("ENC1", "Esta es la encuesta 1", "img1.jpg", true);
            db.insertaEncuesta(en);
            encuestas.Add(en);
            en = new Encuesta("ENC2", "Esta es la encuesta 2", "img2.jpg", true);
            db.insertaEncuesta(en);
            encuestas.Add(en);
            en = new Encuesta("ENC3", "Esta es la encuesta 3", "img3.jpg", true);
            db.insertaEncuesta(en);
            encuestas.Add(en);
            en = new Encuesta("ENC4", "Esta es la encuesta 4", "img4.jpg", false);
            db.insertaEncuesta(en);
            encuestas.Add(en);
            en = new Encuesta("ENC5", "Esta es la encuesta 5", "img5.jpg", false);
            db.insertaEncuesta(en);
            encuestas.Add(en);
            db.insertaRespuesta(new Respuesta(en, Voto.ENFADADO, "Estoy enfadado"));
        }

        /// <summary>
        /// Comprueba que la instancias solicitada es una base de datos son la misma
        /// </summary>
        [TestMethod]
        public void TestSingleton()
        {
            DB db = DB.getDB();
            DB db2 = DB.getDB();
            Assert.AreSame(db,db2);
        }

        /// <summary>
        /// Comprueba que un usuario es cargado correctamente
        /// </summary>
        [TestMethod]
        public void TestCargaUsuarioExistente()
        {
            try
            {
                db.cargaUsuario("Dios");
            }catch(KeyNotFoundException ex)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Comprueba que ante un usuario que no existe
        /// </summary>
        [TestMethod]
        public void TestCargaUsuarioInexistente()
        {
            try
            {
                db.cargaUsuario("NoExisto");
            }
            catch (KeyNotFoundException ex)
            {
                
            }
        }

        /// <summary>
        /// Carga una encuesta que existe
        /// </summary>
        [TestMethod]
        public void TestCargaEncuestaExistente()
        {
            try
            {
                db.cargaEncuesta("ENC1");
            }
            catch (KeyNotFoundException ex)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Carga una encuesta que no existe
        /// </summary>
        [TestMethod]
        public void TestCargaEncuestaInxistente()
        {
            try
            {
                db.cargaEncuesta("ENC0");
                Assert.Fail();
            }
            catch (KeyNotFoundException ex)
            {
                
            }
        }

        /// <summary>
        /// Carga una respuesta que no existe
        /// </summary>
        [TestMethod]
        public void TestCargaRespuestaInexistente()
        {
            try
            {
                db.cargaRespuesta("ENC1",0);
                Assert.Fail();
            }
            catch (KeyNotFoundException ex)
            {

            }
        }

        /// <summary>
        /// Carga una respuesta que existe
        /// </summary>
        [TestMethod]
        public void TestCargaRespuestaExistente()
        {
            try
            {
                db.cargaRespuesta("ENC5", 0);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Carga la lista de encuestas
        /// </summary>
        [TestMethod]
        public void TestCargaEncuestas()
        { 
            List<Encuesta> encuestasDB = db.cargaEncuestas();
            foreach(Encuesta e in this.encuestas)
            {
                Assert.IsTrue(encuestasDB.Contains(e));
            }
        }

        /// <summary>
        /// Inserta una encuesta en la base de datos
        /// </summary>
        [TestMethod]
        public void TestInsertaEncuestas()
        {
            Encuesta en = new Encuesta("ENC6", "Esta es la encuesta 6", "img6.jpg", true);
            db.insertaEncuesta(en);
            Encuesta en2 = db.cargaEncuesta(en.Titulo);
            Assert.AreEqual(en, en2);
        }

        [TestMethod]
        public void TestActualizaEncuesta()
        {
            Encuesta en = this.encuestas[0];
        }

    }
}
