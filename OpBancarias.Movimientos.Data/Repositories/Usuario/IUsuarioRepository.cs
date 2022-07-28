namespace OpBancarias.Data.Repositories.Usuario
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Saves user´s info on database, used to create a new user
        /// </summary>
        /// <param name="usuarioModel">model with customer´s info</param>
        /// <returns></returns>
        Task<Models.Usuario> SaveUsuario(Models.Usuario usuarioModel);

        /// <summary>
        /// Get user´s info querying by username
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns></returns>
        Task<Models.Usuario?> GetUsuario(string userName);
    }
}
