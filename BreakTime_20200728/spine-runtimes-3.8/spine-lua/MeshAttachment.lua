-------------------------------------------------------------------------------
-- Spine Runtimes License Agreement
-- Last updated January 1, 2020. Replaces all prior versions.
--
-- Copyright (c) 2013-2020, Esoteric Software LLC
--
-- Integration of the Spine Runtimes into software or otherwise creating
-- derivative works of the Spine Runtimes is permitted under the terms and
-- conditions of Section 2 of the Spine Editor License Agreement:
-- http://esotericsoftware.com/spine-editor-license
--
-- Otherwise, it is permitted to integrate the Spine Runtimes into software
-- or otherwise create derivative works of the Spine Runtimes (collectively,
-- "Products"), provided that each user of the Products must obtain their own
-- Spine Editor license and redistribution of the Products in any form must
-- include this license and copyright notice.
--
-- THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
-- EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
-- WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
-- DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
-- DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
-- (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
-- BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
-- ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
-- (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
-- THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
-------------------------------------------------------------------------------

local AttachmentType = require "spine-lua.AttachmentType"

local MeshAttachment = {}
function MeshAttachment.new (name)
	if not name then error("name cannot be nil", 2) end

	local self = {
		name = name,
		type = AttachmentType.mesh,
		vertices = nil,
		uvs = nil,
		regionUVs = nil,
		triangles = nil,
		hullLength = 0,
		r = 1, g = 1, b = 1, a = 1,
		path = nil,
		rendererObject = nil,
		regionU = 0, regionV = 0, regionU2 = 1, regionV2 = 1, regionRotate = false,
		regionOffsetX = 0, regionOffsetY = 0,
		regionWidth = 0, regionHeight = 0,
		regionOriginalWidth = 0, regionOriginalHeight = 0,
		edges = nil,
		width = 0, height = 0
	}

	function self:updateUVs ()
		local width, height = self.regionU2 - self.regionU, self.regionV2 - self.regionV
		local n = #self.regionUVs
		if not self.uvs or #self.uvs ~= n then
			self.uvs = {}
		end
		if self.regionRotate then
			for i = 1, n, 2 do
				self.uvs[i] = self.regionU + self.regionUVs[i + 1] * width
				self.uvs[i + 1] = self.regionV + height - self.regionUVs[i] * height
			end
		else
			for i = 1, n, 2 do
				self.uvs[i] = self.regionU + self.regionUVs[i] * width
				self.uvs[i + 1] = self.regionV + self.regionUVs[i + 1] * height
			end
		end
	end

	function self:computeWorldVertices (x, y, slot, worldVertices)
		local bone = slot.bone
x,y=slot.bone.skeleton.x,slot.bone.skeleton.y
		x = x + bone.worldX
		y = y + bone.worldY
		local m00, m01, m10, m11 = bone.m00, bone.m01, bone.m10, bone.m11
		local vertices = self.vertices
		local verticesCount = #vertices
		if slot.deform and #slot.deform == verticesCount then vertices = slot.deform end
		for i = 1, verticesCount, 2 do
			local vx = vertices[i]
			local vy = vertices[i + 1]
			worldVertices[i] = vx * m00 + vy * m01 + x
			worldVertices[i + 1] = vx * m10 + vy * m11 + y
		end
	end

	return self
end
return MeshAttachment
