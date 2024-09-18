using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

internal class GlitchBlitPass : ScriptableRenderPass
{
    ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Glitch");
    Material m_Material;
    RTHandle m_CameraColorTarget;
    float m_shakeRate;
    float m_shakeSpeed;
    float m_shakeBlockSize;
    float m_shakeColorRate;

    public GlitchBlitPass(Material material)
    {
        m_Material = material;
        renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public void SetTarget(RTHandle colorHandle, float shakeRate, float shakeSpeed, float shakeBlockSize, float shakeColorRate)
    {
        m_CameraColorTarget = colorHandle;
        m_shakeRate = shakeRate;
        m_shakeSpeed = shakeSpeed;
        m_shakeBlockSize = shakeBlockSize;
        m_shakeColorRate = shakeColorRate;

    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        ConfigureTarget(m_CameraColorTarget);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var cameraData = renderingData.cameraData;
        if (cameraData.camera.cameraType != CameraType.Game)
            return;

        if (m_Material == null)
            return;

        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, m_ProfilingSampler))
        {
            m_Material.SetFloat("_ShakeRate", m_shakeRate);
            m_Material.SetFloat("_ShakeSpeed", m_shakeSpeed);
            m_Material.SetFloat("_ShakeBlockSize", m_shakeBlockSize);
            m_Material.SetFloat("_ShakeColorRate", m_shakeColorRate);
            Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, m_CameraColorTarget, m_Material, 0);
        }
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();

        CommandBufferPool.Release(cmd);
    }
}