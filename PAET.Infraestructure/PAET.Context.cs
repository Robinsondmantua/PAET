﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAET.Infraestructure
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestEntities : DbContext
    {
        public TestEntities()
            : base("name=TestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Candidatos> Candidatos { get; set; }
        public virtual DbSet<CategoriaProfesional> CategoriaProfesional { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<ChatEntrevistaCandidato> ChatEntrevistaCandidato { get; set; }
        public virtual DbSet<Dificultad> Dificultad { get; set; }
        public virtual DbSet<EntrevistaCandidato> EntrevistaCandidato { get; set; }
        public virtual DbSet<EntrevistaPregunta> EntrevistaPregunta { get; set; }
        public virtual DbSet<EntrevistaPreguntaTest> EntrevistaPreguntaTest { get; set; }
        public virtual DbSet<Entrevistas> Entrevistas { get; set; }
        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<Multimedia> Multimedia { get; set; }
        public virtual DbSet<MultimediaEntrevistaCandidato> MultimediaEntrevistaCandidato { get; set; }
        public virtual DbSet<Preguntas> Preguntas { get; set; }
        public virtual DbSet<Respuestas> Respuestas { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tecnologia> Tecnologia { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoPregunta> TipoPregunta { get; set; }
        public virtual DbSet<Titulacion> Titulacion { get; set; }
        public virtual DbSet<UsuariosAdmin> UsuariosAdmin { get; set; }
        public virtual DbSet<Valoracion> Valoracion { get; set; }
        public virtual DbSet<VwEntrevistaCandidatos> VwEntrevistaCandidatos { get; set; }
        public virtual DbSet<VwEntrevistasActivas> VwEntrevistasActivas { get; set; }
    }
}
